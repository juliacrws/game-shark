using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameShark.Infrastructure.Persistence;

namespace GameShark.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin, Gerente")]
public class RadarController : Controller
{
    private readonly AppDbContext _context;

    public RadarController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Puxa todos os alertas que ainda não foram resolvidos, junto com as infos do Produto
        var alertas = await _context.AlertasEstoque
            .Include(a => a.Produto)
            .Where(a => !a.StatusResolvido)
            .OrderByDescending(a => a.DataSolicitacao)
            .ToListAsync();

        return View(alertas);
    }

    // Botão para você marcar que já resolveu/avisou o cliente
    [HttpPost]
    // Botão para você marcar que já resolveu/avisou o cliente
    [HttpPost]
    public async Task<IActionResult> MarcarResolvido(int id)
    {
        // Puxamos o alerta e já INCLUÍMOS o Produto para saber o Nome do jogo
        var alerta = await _context.AlertasEstoque
            .Include(a => a.Produto)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (alerta != null)
        {
            // Marca como resolvido no banco
            alerta.StatusResolvido = true;
            await _context.SaveChangesAsync();

            // 🚀 DISPARA O E-MAIL!
            if (alerta.Produto != null)
            {
                // 👇 Chamando o serviço na pasta correta!
                await GameShark.Web.Services.HackerMailService.EnviarAlertaEstoqueAsync(alerta.EmailCliente, alerta.Produto.Nome);
            }
        }

        return RedirectToAction(nameof(Index));
    }
}