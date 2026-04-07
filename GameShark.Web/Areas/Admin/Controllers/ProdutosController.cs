using System.IO;
using GameShark.Domain.Entities;
using GameShark.Domain.Enums;
using GameShark.Infrastructure.Persistence;
using GameShark.Web.Areas.Admin.Models;
using GameShark.Web.Helpers; // 👈 Importando a nossa classe de segurança!
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameShark.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProdutosController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public ProdutosController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task<IActionResult> Index() => View(await _context.Produtos.Include(p => p.Categoria).Include(p => p.Plataforma).ToListAsync());

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var produto = await _context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.Plataforma)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (produto == null) return NotFound();

        return View(produto);
    }

    public async Task<IActionResult> Create()
    {
        await SetLists();
        return View(new ProdutoEditVm());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProdutoEditVm model)
    {
        if (!ModelState.IsValid)
        {
            await SetLists(model.CategoriaId, model.PlataformaId, model.Classificacao);
            return View(model);
        }

        // 👇 BLINDAGEM NO CREATE
        if (model.Capa != null)
        {
            if (!ImageSecurity.IsValidImage(model.Capa))
            {
                ModelState.AddModelError("Capa", "Alerta de Segurança: Arquivo inválido. Use apenas JPG, PNG ou WEBP até 5MB.");
                await SetLists(model.CategoriaId, model.PlataformaId, model.Classificacao);
                return View(model);
            }
        }

        var p = new Produto
        {
            Nome = model.Nome,
            Preco = model.Preco,
            CategoriaId = model.CategoriaId,
            PlataformaId = model.PlataformaId,
            Descricao = model.Descricao,
            Estoque = model.Estoque,
            Classificacao = model.Classificacao
        };

        if (model.Capa != null) p.ImagemUrl = await SaveFile(model.Capa);

        _context.Add(p);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var p = await _context.Produtos.FindAsync(id);
        if (p == null) return NotFound();

        await SetLists(p.CategoriaId, p.PlataformaId, p.Classificacao);

        return View(new ProdutoEditVm
        {
            Id = p.Id,
            Nome = p.Nome,
            Preco = p.Preco,
            Estoque = p.Estoque,
            Descricao = p.Descricao,
            ImagemUrl = p.ImagemUrl,
            CategoriaId = p.CategoriaId,
            PlataformaId = p.PlataformaId,
            Classificacao = p.Classificacao
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProdutoEditVm model)
    {
        if (!ModelState.IsValid)
        {
            await SetLists(model.CategoriaId, model.PlataformaId, model.Classificacao);
            return View(model);
        }

        // 👇 BLINDAGEM NO EDIT
        if (model.Capa != null)
        {
            if (!ImageSecurity.IsValidImage(model.Capa))
            {
                ModelState.AddModelError("Capa", "Alerta de Segurança: Arquivo inválido. Use apenas JPG, PNG ou WEBP até 5MB.");
                await SetLists(model.CategoriaId, model.PlataformaId, model.Classificacao);
                return View(model);
            }
        }

        var p = await _context.Produtos.FindAsync(model.Id);
        if (p == null) return NotFound();

        p.Nome = model.Nome;
        p.Preco = model.Preco;
        p.Estoque = model.Estoque;
        p.Descricao = model.Descricao;
        p.CategoriaId = model.CategoriaId;
        p.PlataformaId = model.PlataformaId;
        p.Classificacao = model.Classificacao;

        if (model.Capa != null) p.ImagemUrl = await SaveFile(model.Capa);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var p = await _context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.Plataforma)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (p == null) return NotFound();

        return View(p);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken, Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var p = await _context.Produtos.FindAsync(id);
        if (p != null) _context.Produtos.Remove(p);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task SetLists(int? categoriaIdSelecionada = null, int? plataformaIdSelecionada = null, ClassificacaoIndicativa? classificacaoSelecionada = null)
    {
        ViewBag.CategoriaId = new SelectList(await _context.Categorias.ToListAsync(), "Id", "Nome", categoriaIdSelecionada);
        ViewBag.PlataformaId = new SelectList(await _context.Plataformas.ToListAsync(), "Id", "Nome", plataformaIdSelecionada);

        var listaClassificacoes = Enum.GetValues(typeof(ClassificacaoIndicativa))
                                      .Cast<ClassificacaoIndicativa>()
                                      .Select(c => new { Id = (int)c, Nome = c.ToString() });

        ViewBag.Classificacoes = new SelectList(listaClassificacoes, "Id", "Nome", classificacaoSelecionada != null ? (int)classificacaoSelecionada : null);

        ViewBag.Categorias = ViewBag.CategoriaId;
        ViewBag.Plataformas = ViewBag.PlataformaId;
    }

    private async Task<string> SaveFile(IFormFile file)
    {
        var name = Guid.NewGuid() + "_" + file.FileName;

        string rootPath = string.IsNullOrWhiteSpace(_env.WebRootPath)
            ? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")
            : _env.WebRootPath;

        var pastaDestino = Path.Combine(rootPath, "images", "produtos");

        if (!Directory.Exists(pastaDestino))
        {
            Directory.CreateDirectory(pastaDestino);
        }

        var caminhoCompleto = Path.Combine(pastaDestino, name);

        using var stream = new FileStream(caminhoCompleto, FileMode.Create);
        await file.CopyToAsync(stream);

        return "/images/produtos/" + name;
    }
}