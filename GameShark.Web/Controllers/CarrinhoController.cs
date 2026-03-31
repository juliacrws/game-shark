using System.Security.Claims;
using GameShark.Domain.Entities;
using GameShark.Infrastructure.Persistence;
using GameShark.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShark.Web.Controllers;

[Authorize]
public class CarrinhoController : Controller
{
    private readonly AppDbContext _context;

    public CarrinhoController(AppDbContext context)
    {
        _context = context;
    }

    private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    public async Task<IActionResult> Index()
    {
        var userId = GetUserId();

        var itensBanco = await _context.CarrinhoItems
            .Include(c => c.Produto)
                .ThenInclude(p => p!.Plataforma) // 👈 Adicione o '!' logo depois do 'p' aqui
            .Where(c => c.UsuarioId == userId)
            .ToListAsync();

        var vm = itensBanco
                    .Where(i => i.Produto != null)
                    .Select(i => new CarrinhoItemVm
                    {
                        ProdutoId = i.ProdutoId,
                        Nome = i.Produto!.Nome,
                        Preco = i.Produto!.Preco,
                        ImagemUrl = i.Produto!.ImagemUrl ?? "",
                        Quantidade = i.Quantidade,
                        Plataforma = i.Produto!.Plataforma?.Nome ?? "Digital",
                        // 👇 O '!' aqui garante pro C# que o Produto não é nulo e mata o aviso
                        EstoqueDisponivel = i.Produto!.Estoque
                    }).ToList();

        ViewBag.Total = vm.Sum(i => i.SubTotal);
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar(int id)
    {
        var userId = GetUserId();
        var produto = await _context.Produtos.FindAsync(id);

        if (produto == null || produto.Estoque <= 0) return NotFound();

        var itemExistente = await _context.CarrinhoItems
            .FirstOrDefaultAsync(c => c.UsuarioId == userId && c.ProdutoId == id);

        if (itemExistente != null)
        {
            // Só adiciona se tiver estoque
            if (itemExistente.Quantidade < produto.Estoque)
            {
                itemExistente.Quantidade++;
            }
            else
            {
                TempData["MensagemErro"] = "Estoque máximo atingido para este item!";
            }
        }
        else
        {
            _context.CarrinhoItems.Add(new CarrinhoItem
            {
                UsuarioId = userId,
                ProdutoId = id,
                Quantidade = 1
            });
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    // 👇 O NOVO MÉTODO PARA OS BOTÕES + E - NA TELA DO CARRINHO
    [HttpPost]
    public async Task<IActionResult> AtualizarQuantidade(int produtoId, string acao)
    {
        var userId = GetUserId();
        var itemCarrinho = await _context.CarrinhoItems
            .Include(c => c.Produto)
            .FirstOrDefaultAsync(c => c.ProdutoId == produtoId && c.UsuarioId == userId);

        if (itemCarrinho != null && itemCarrinho.Produto != null)
        {
            if (acao == "aumentar")
            {
                if (itemCarrinho.Quantidade < itemCarrinho.Produto.Estoque)
                {
                    itemCarrinho.Quantidade++;
                }
                else
                {
                    TempData["MensagemErro"] = "Estoque máximo atingido!";
                }
            }
            else if (acao == "diminuir")
            {
                if (itemCarrinho.Quantidade > 1)
                {
                    itemCarrinho.Quantidade--;
                }
                else
                {
                    _context.CarrinhoItems.Remove(itemCarrinho);
                    TempData["MensagemSucesso"] = "Item removido do inventário.";
                }
            }
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Remover(int id) // Mudei para HttpPost por segurança
    {
        var userId = GetUserId();
        var item = await _context.CarrinhoItems
            .FirstOrDefaultAsync(c => c.UsuarioId == userId && c.ProdutoId == id);

        if (item != null)
        {
            _context.CarrinhoItems.Remove(item);
            await _context.SaveChangesAsync();
            TempData["MensagemSucesso"] = "Item descartado do loot.";
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> FinalizarCompra()
    {
        var userId = GetUserId();

        var itensCarrinho = await _context.CarrinhoItems
            .Include(c => c.Produto)
            .Where(c => c.UsuarioId == userId)
            .ToListAsync();

        if (!itensCarrinho.Any()) return RedirectToAction("Index");

        decimal valorTotal = itensCarrinho.Sum(i => i.Quantidade * i.Produto!.Preco);

        var pedido = new Pedido
        {
            UsuarioId = userId,
            DataPedido = DateTime.Now,
            ValorTotal = valorTotal,
            Status = "Aprovado"
        };

        foreach (var item in itensCarrinho)
        {
            pedido.Itens.Add(new PedidoItem
            {
                ProdutoId = item.ProdutoId,
                Quantidade = item.Quantidade,
                PrecoUnitario = item.Produto!.Preco
            });

            item.Produto!.Estoque -= item.Quantidade; // Abate o estoque
        }

        _context.Pedidos.Add(pedido);
        _context.CarrinhoItems.RemoveRange(itensCarrinho);
        await _context.SaveChangesAsync();

        TempData["MensagemSucesso"] = $"Loot garantido! O teu pedido #{pedido.Id} foi registado com sucesso.";
        return RedirectToAction("Index", "Home");
    }
}