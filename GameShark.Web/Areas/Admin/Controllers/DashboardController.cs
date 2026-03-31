using GameShark.Web.Areas.Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameShark.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")] // 👈 A FECHADURA MÁGICA! Só entra quem tem a role "Admin"
public class DashboardController : Controller
{
    private readonly DashboardService _service;
    public DashboardController(DashboardService service) => _service = service;

    public async Task<IActionResult> Index()
    {
        var stats = await _service.GetStatsAsync();
        return View(stats);
    }
}