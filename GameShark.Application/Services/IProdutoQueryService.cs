using GameShark.Application.DTOs;
using GameShark.Application.Filters;

namespace GameShark.Application.Services;

public interface IProdutoQueryService
{
    Task<Paging<ProdutoDto>> GetCatalogAsync(ProdutoFilter filter);
}