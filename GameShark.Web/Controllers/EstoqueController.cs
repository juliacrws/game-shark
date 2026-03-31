using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameShark.Infrastructure.Persistence;

namespace GameShark.Web.Areas.Admin.Controllers;

[Area("Admin")] // 👈 Diz que pertence à Boss Room
[Authorize(Roles = "Admin, Estoquista")] // 👈 Apenas Donos e Estoquistas entram aqui!
public class EstoqueController : Controller
{
    private readonly AppDbContext _context;

    public EstoqueController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Trazemos os produtos ordenados para mostrar os que têm MENOS estoque primeiro
        var produtos = await _context.Produtos
            .Include(p => p.Plataforma)
            .OrderBy(p => p.Estoque)
            .ToListAsync();

        return View(produtos);
    }

    [HttpPost]
    public async Task<IActionResult> Atualizar(int id, int novaQuantidade)
    {
        var produto = await _context.Produtos.FindAsync(id);

        if (produto != null)
        {
            // Proteção para ninguém colocar estoque negativo por engano
            produto.Estoque = novaQuantidade >= 0 ? novaQuantidade : 0;

            await _context.SaveChangesAsync();

            // Dispara o nosso Toast Neon!
            TempData["MensagemSucesso"] = $"Estoque de {produto.Nome} atualizado para {produto.Estoque} unidades!";
        }

        return RedirectToAction(nameof(Index));
    }
}