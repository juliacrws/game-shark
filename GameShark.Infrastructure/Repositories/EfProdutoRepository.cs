using GameShark.Application.Abstractions.Repositories;
using GameShark.Application.DTOs;
using GameShark.Application.Filters;
using GameShark.Domain.Entities;
using GameShark.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GameShark.Infrastructure.Repositories;

public class EfProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;
    public EfProdutoRepository(AppDbContext context) => _context = context;

    public async Task<Paging<Produto>> GetPagedProdutosAsync(ProdutoFilter filter)
    {
        var query = _context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.Plataforma)
            .AsQueryable();

        if (filter.CategoriaId.HasValue)
            query = query.Where(p => p.CategoriaId == filter.CategoriaId);

        if (filter.PlataformaId.HasValue)
            query = query.Where(p => p.PlataformaId == filter.PlataformaId);

        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            query = query.Where(p => p.Nome.Contains(filter.SearchTerm));

        var total = await query.CountAsync();

        var items = await query
            .OrderBy(p => p.Nome)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new Paging<Produto> { TotalItems = total, Page = filter.Page, PageSize = filter.PageSize, Items = items };
    }

    public async Task<Produto?> GetByIdAsync(int id)
    {
        return await _context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.Plataforma)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}