using System.Diagnostics;
using GameShark.Domain.Entities;
using GameShark.Infrastructure.Persistence;
using GameShark.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShark.Web.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Puxa os últimos 8 produtos adicionados, incluindo as categorias e plataformas
        var ultimosLançamentos = await _context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.Plataforma)
            .OrderByDescending(p => p.Id)
            .Take(8)
            .ToListAsync();

        return View(ultimosLançamentos);
    }
    public async Task<IActionResult> Details(int id)
    {
        var produto = await _context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.Plataforma)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (produto == null) return NotFound();

        return View(produto);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}