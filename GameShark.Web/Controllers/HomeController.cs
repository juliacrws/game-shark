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
    public async Task<IActionResult> AviseMe(int produtoId, string emailCliente)
    {
        // 1. Cria o registro do alerta
        var alerta = new AlertaEstoque
        {
            ProdutoId = produtoId,
            EmailCliente = emailCliente,
            DataSolicitacao = DateTime.Now,
            StatusResolvido = false
        };

        // 2. Salva no banco de dados
        _context.AlertasEstoque.Add(alerta);
        await _context.SaveChangesAsync();

        // 3. Avisa o jogador
        TempData["MensagemSucesso"] = $"Radar ativado! Avisaremos em {emailCliente} assim que o loot chegar.";
        return RedirectToAction(nameof(Index));
    }

    // 👇 O ORÁCULO DE LOOT (A página com o formulário)
    // Se o seu arquivo HTML principal chama Quiz.cshtml, mantenha este nome. 
    // Se chama Oraculo.cshtml, mude o nome do método para Oraculo().
    public IActionResult Quiz()
    {
        return View();
    }

    // 👇 PROCESSAMENTO DO ALGORITMO (Quando o botão é clicado)
    [HttpPost]
    public async Task<IActionResult> Recomendacao(string vibe, string plataforma)
    {
        var query = _context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.Plataforma)
            .Where(p => p.Estoque > 0) // Excelente regra: só sugere o que tem em estoque!
            .AsQueryable();

        if (!string.IsNullOrEmpty(vibe))
        {
            query = query.Where(p => p.Categoria!.Nome.Contains(vibe));
        }

        if (!string.IsNullOrEmpty(plataforma))
        {
            query = query.Where(p => p.Plataforma!.Nome.Contains(plataforma));
        }

        // Guid.NewGuid() embaralha a lista para o algoritmo sempre dar sugestões variadas!
        var recomendados = await query
            .OrderBy(r => Guid.NewGuid())
            .Take(3) // Pegar 3 fica perfeito visualmente no grid do Bootstrap
            .ToListAsync();

        // Enviando os dados para a interface usar
        ViewData["Vibe"] = vibe;
        ViewData["Plataforma"] = plataforma;

        ViewData["MensagemOraculo"] = recomendados.Any()
            ? "O Oráculo analisou seu perfil e encontrou estes matches perfeitos:"
            : "Sua vibe é tão única que nosso estoque atual não deu match. Mas novas naves chegam amanhã!";

        // Retorna a view de Resultado. Certifique-se de que o arquivo se chama "ResultadoOraculo.cshtml" 
        // ou mude aqui para o nome exato do arquivo que você criou para exibir a resposta.
        return View("ResultadoOraculo", recomendados);
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