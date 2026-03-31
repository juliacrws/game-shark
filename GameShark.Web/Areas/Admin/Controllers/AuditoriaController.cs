using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameShark.Infrastructure.Persistence;

namespace GameShark.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin, Gerente")] // Só a alta cúpula vê os logs
public class AuditoriaController : Controller
{
    private readonly AppDbContext _context;

    public AuditoriaController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Traz os últimos 50 acessos, do mais recente para o mais antigo
        var logs = await _context.LogsAcesso
            .OrderByDescending(l => l.DataHora)
            .Take(50)
            .ToListAsync();

        return View(logs);
    }
}