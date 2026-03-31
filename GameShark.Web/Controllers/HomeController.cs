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

    // 👇 Agora o Index recebe o que o usuário digitou na barra
    public async Task<IActionResult> Index(string? busca)
    {
        // Devolve o termo para a View manter o texto na barra de pesquisa
        ViewData["BuscaAtual"] = busca;

        // Prepara a consulta base
        var query = _context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.Plataforma)
            .AsQueryable();

        // Se o usuário digitou algo, a gente filtra!
        if (!string.IsNullOrEmpty(busca))
        {
            query = query.Where(p => p.Nome.Contains(busca) || 
                                     p.Plataforma!.Nome.Contains(busca) || // 👈 Adicionado o '!'
                                     p.Categoria!.Nome.Contains(busca));   // 👈 Adicionado o '!'
        }

        // Executa a busca e traz os 20 últimos resultados
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

    // 👇 O Backend do Botão Avise-me!
    [HttpPost]
    public IActionResult AviseMe(int produtoId, string emailCliente)
    {
        // Aqui, no futuro, você pode salvar na tabela de e-mails para disparar marketing.
        // Por enquanto, vamos dar o feedback visual de sucesso com o nosso Toast Neon!
        TempData["MensagemSucesso"] = $"Radar ativado! Avisaremos em {emailCliente} assim que o loot chegar.";
        return RedirectToAction(nameof(Index));
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