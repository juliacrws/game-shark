using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameShark.Infrastructure.Persistence;
using GameShark.Web.Areas.Admin.Models;

namespace GameShark.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin, Gerente")] // 👈 Dinheiro é assunto da alta cúpula
public class CaixaController : Controller
{
    private readonly AppDbContext _context;

    public CaixaController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var hoje = DateTime.Today;
        var inicioMes = new DateTime(hoje.Year, hoje.Month, 1);

        // Trazemos as vendas ordenadas da mais recente para a mais antiga
        var todosPedidos = await _context.Pedidos
            .Where(p => p.Status == "Aprovado")
            .OrderByDescending(p => p.DataPedido)
            .ToListAsync();

        var vm = new CaixaVm
        {
            FaturamentoTotal = todosPedidos.Sum(p => p.ValorTotal),

            // Filtra só as vendas que aconteceram do dia 1º deste mês até agora
            FaturamentoMes = todosPedidos.Where(p => p.DataPedido >= inicioMes).Sum(p => p.ValorTotal),

            // Filtra só as vendas que aconteceram HOJE
            FaturamentoHoje = todosPedidos.Where(p => p.DataPedido.Date == hoje).Sum(p => p.ValorTotal),

            // Pega as últimas 30 vendas para listar na tabela
            UltimasVendas = todosPedidos.Take(30).ToList()
        };

        return View(vm);
    }
}