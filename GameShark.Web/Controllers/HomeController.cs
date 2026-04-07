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

    public async Task<IActionResult> Index(string? busca)
    {
        ViewData["BuscaAtual"] = busca;

        var query = _context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.Plataforma)
            .AsQueryable();

        if (!string.IsNullOrEmpty(busca))
        {
            query = query.Where(p => p.Nome.Contains(busca) ||
                                     p.Plataforma!.Nome.Contains(busca) ||
                                     p.Categoria!.Nome.Contains(busca));
        }

        var produtos = await query
            .OrderByDescending(p => p.Id)
            .Take(20)
            .ToListAsync();

        return View(produtos);
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

    [HttpPost]
    public IActionResult AviseMe(int produtoId, string emailCliente)
    {
        TempData["MensagemSucesso"] = $"Radar ativado! Avisaremos em {emailCliente} assim que o loot chegar.";
        return RedirectToAction(nameof(Index));
    }

    // 👇 O ORÁCULO DE LOOT (QUIZ)
    public IActionResult Quiz()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Recomendacao(string vibe, string plataforma)
    {
        var query = _context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.Plataforma)
            .Where(p => p.Estoque > 0)
            .AsQueryable();

        if (!string.IsNullOrEmpty(vibe))
        {
            query = query.Where(p => p.Categoria!.Nome.Contains(vibe));
        }

        if (!string.IsNullOrEmpty(plataforma))
        {
            query = query.Where(p => p.Plataforma!.Nome.Contains(plataforma));
        }

        var recomendados = await query.Take(4).ToListAsync();

        ViewData["MensagemOraculo"] = recomendados.Any()
            ? "O Oráculo analisou seu perfil e encontrou estes matches perfeitos:"
            : "Sua vibe é tão única que nosso estoque atual não deu match. Mas novas naves chegam amanhã!";

        return View("Recomendacao", recomendados);
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