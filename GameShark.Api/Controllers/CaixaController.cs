using Microsoft.AspNetCore.Mvc;
using GameShark.Infrastructure.Persistence;
using GameShark.Domain.Entities;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GameShark.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CaixaController : ControllerBase
{
    private readonly AppDbContext _context;

    public CaixaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("status")]
    public IActionResult VerificarStatus()
    {
        return Ok(new { estaAberto = true, saldoAtual = 150.00m, operador = "Admin" });
    }

    [HttpPost("abrir")]
    public IActionResult AbrirCaixa([FromBody] AbrirCaixaDto request)
    {
        if (request.ValorInicial < 0) return BadRequest(new { erro = "Valor inválido." });
        return Ok(new { mensagem = "🟢 Caixa aberto com sucesso!", saldo = request.ValorInicial });
    }

    // 🚀 A MÁGICA DO ESTOQUE ACONTECE AQUI! 
    [HttpPost("movimentacao")]
    public async Task<IActionResult> RegistrarMovimentacao([FromBody] MovimentacaoDto request)
    {
        if (request.Tipo == "Entrada")
        {
            var vendaFisica = new Pedido
            {
                DataPedido = DateTime.Now,
                Status = "Concluido",
                ValorTotal = request.Valor,
                Itens = new List<PedidoItem>() // 👈 Agora o pedido tem uma lista de itens!
            };

            // 🔄 Roda o carrinho inteiro, baixando o estoque de cada um
            foreach (var item in request.Itens)
            {
                var produto = await _context.Produtos.FindAsync(item.ProdutoId);
                if (produto != null)
                {
                    // 🔥 AQUI É A BAIXA DO ESTOQUE 🔥
                    produto.Estoque -= item.Quantidade;
                }

                // Salva o que foi vendido para o histórico
                vendaFisica.Itens.Add(new PedidoItem
                {
                    ProdutoId = item.ProdutoId,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = item.PrecoUnitario
                });
            }

            _context.Pedidos.Add(vendaFisica);
            await _context.SaveChangesAsync();
        }

        return Ok(new { mensagem = "💸 Movimentação e Baixa de Estoque registradas!" });
    }
}

// 📦 DTOs ATUALIZADOS
public class AbrirCaixaDto { public decimal ValorInicial { get; set; } }

public class ItemVendaDto
{
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
}

public class MovimentacaoDto
{
    public decimal Valor { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public List<ItemVendaDto> Itens { get; set; } = new List<ItemVendaDto>(); // 👈 Recebe o carrinho
}