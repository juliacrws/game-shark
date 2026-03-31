using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameShark.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")] // 👈 A FECHADURA MÁGICA! Só entra quem tem a role "Admin"
public class ManageController : Controller
{
    public IActionResult Index() => View();
}