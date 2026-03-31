using GameShark.Domain.Entities;
using GameShark.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShark.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")] // 👈 A FECHADURA MÁGICA! Só entra quem tem a role "Admin"
public class CategoriasController : Controller
{
    private readonly AppDbContext _context;
    public CategoriasController(AppDbContext context) => _context = context;

    public async Task<IActionResult> Index() => View(await _context.Categorias.ToListAsync());

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Categoria model)
    {
        if (!ModelState.IsValid) return View(model);
        _context.Categorias.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await _context.Categorias.FindAsync(id);
        return item == null ? NotFound() : View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Categoria model)
    {
        if (!ModelState.IsValid) return View(model);
        _context.Update(model);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Categorias.FindAsync(id);
        return item == null ? NotFound() : View(item);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken, Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await _context.Categorias.FindAsync(id);
        if (item != null) _context.Categorias.Remove(item);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}