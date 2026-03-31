using GameShark.Infrastructure.Entities;
using GameShark.Web.Areas.Admin.Models; // Necessário para o UsuarioCreateVm
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShark.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")] // 👈 A FECHADURA MÁGICA! Só entra quem tem a role "Admin"
public class UsuariosController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    public UsuariosController(UserManager<ApplicationUser> userManager) => _userManager = userManager;

    public async Task<IActionResult> Index() => View(await _userManager.Users.ToListAsync());

    // GET: Create (Para você recrutar Staff ou forçar a criação de um Player)
    public IActionResult Create()
    {
        ViewBag.Cargos = new[] { "Admin", "Gerente", "Funcionario", "Player" };
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UsuarioCreateVm model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Cargos = new[] { "Admin", "Gerente", "Funcionario", "Player" };
            return View(model);
        }

        var user = new ApplicationUser { UserName = model.Email, Email = model.Email, NomeCompleto = model.NomeCompleto };
        var result = await _userManager.CreateAsync(user, model.Senha);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, model.Cargo);
            return RedirectToAction(nameof(Index));
        }

        foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);

        ViewBag.Cargos = new[] { "Admin", "Gerente", "Funcionario", "Player" };
        return View(model);
    }

    public async Task<IActionResult> Edit(string id) => View(await _userManager.FindByIdAsync(id));

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ApplicationUser model)
    {
        var user = await _userManager.FindByIdAsync(model.Id);
        if (user == null) return NotFound();
        user.NomeCompleto = model.NomeCompleto;
        user.Email = model.Email;
        await _userManager.UpdateAsync(user);
        return RedirectToAction(nameof(Index));
    }

    // GET: Delete (Mostra a tela de confirmação com a caveira)
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();
        return View(user);
    }

    // POST: Confirmar Exclusão (O Ban Hammer real)
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null) await _userManager.DeleteAsync(user);
        return RedirectToAction(nameof(Index));
    }
}