using GameShark.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GameShark.Web.Areas.Admin.Services;

// O DTO que vai carregar os dados para a tela
public class DashboardDto
{
    public int TotalProdutos { get; set; }
    public int TotalCategorias { get; set; }
    public int TotalUsuarios { get; set; }
    public object ProdutosPorCategoria { get; set; } = null!;
    public object ProdutosPorPlataforma { get; set; } = null!;
    public List<string> UltimosProdutos { get; set; } = new();
}

public class DashboardService
{
    private readonly AppDbContext _context;

    public DashboardService(AppDbContext context) => _context = context;

    public async Task<DashboardDto> GetStatsAsync()
    {
        var dto = new DashboardDto
        {
            TotalProdutos = await _context.Produtos.CountAsync(),
            TotalCategorias = await _context.Categorias.CountAsync(),
            TotalUsuarios = await _context.Users.CountAsync()
        };

        // Agrupamentos para os gráficos do Chart.js
        dto.ProdutosPorCategoria = await _context.Produtos
            .GroupBy(p => p.Categoria!.Nome)
            .Select(g => new { label = g.Key, count = g.Count() })
            .ToListAsync();

        dto.ProdutosPorPlataforma = await _context.Produtos
            .GroupBy(p => p.Plataforma!.Nome)
            .Select(g => new { label = g.Key, count = g.Count() })
            .ToListAsync();

        // Últimos 5 itens cadastrados
        dto.UltimosProdutos = await _context.Produtos
            .OrderByDescending(p => p.Id)
            .Take(5)
            .Select(p => p.Nome)
            .ToListAsync();

        return dto;
    }
}