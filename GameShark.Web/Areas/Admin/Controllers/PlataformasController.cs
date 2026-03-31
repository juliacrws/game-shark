using GameShark.Domain.Entities;
using GameShark.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShark.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")] // 👈 A FECHADURA MÁGICA! Só entra quem tem a role "Admin"
public class PlataformasController : Controller
{
    private readonly AppDbContext _context;

    public PlataformasController(AppDbContext context) => _context = context;

    // GET: Listar
    public async Task<IActionResult> Index() => View(await _context.Plataformas.ToListAsync());

    // GET: Criar
    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Plataforma model)
    {
        if (!ModelState.IsValid) return View(model);
        _context.Plataformas.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Editar
    public async Task<IActionResult> Edit(int id)
    {
        var item = await _context.Plataformas.FindAsync(id);
        return item == null ? NotFound() : View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Plataforma model)
    {
        if (!ModelState.IsValid) return View(model);
        _context.Update(model);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Deletar (Protegido apenas para Admin)
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Plataformas.FindAsync(id);
        return item == null ? NotFound() : View(item);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken, Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await _context.Plataformas.FindAsync(id);
        if (item != null) _context.Plataformas.Remove(item);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}