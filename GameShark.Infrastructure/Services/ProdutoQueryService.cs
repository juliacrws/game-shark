using GameShark.Application.Abstractions.Repositories;
using GameShark.Application.DTOs;
using GameShark.Application.Filters;
using GameShark.Application.Services;

namespace GameShark.Infrastructure.Services;

public class ProdutoQueryService : IProdutoQueryService
{
    private readonly IProdutoRepository _produtoRepo;

    public ProdutoQueryService(IProdutoRepository produtoRepo)
    {
        _produtoRepo = produtoRepo;
    }

    public async Task<Paging<ProdutoDto>> GetCatalogAsync(ProdutoFilter filter)
    {
        var result = await _produtoRepo.GetPagedProdutosAsync(filter);

        var dtos = result.Items.Select(p => new ProdutoDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Descricao = p.Descricao,
            Preco = p.Preco,
            ImagemUrl = p.ImagemUrl,
            CategoriaNome = p.Categoria?.Nome ?? "Sem Categoria",
            PlataformaNome = p.Plataforma?.Nome ?? "Sem Plataforma"
        });

        return new Paging<ProdutoDto>
        {
            TotalItems = result.TotalItems,
            Page = result.Page,
            PageSize = result.PageSize,
            Items = dtos
        };
    }
}