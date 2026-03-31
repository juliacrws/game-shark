using GameShark.Web.Models;

namespace GameShark.Web.Services;

public interface IApiClient
{
    Task<Paging<ProdutoDto>?> GetProdutosAsync(string? searchTerm, int? categoriaId, int? plataformaId, int page = 1);
    Task<IEnumerable<CategoriaDto>?> GetCategoriasAsync();
    Task<IEnumerable<PlataformaDto>?> GetPlataformasAsync();
}