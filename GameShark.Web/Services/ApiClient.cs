using GameShark.Web.Models;

namespace GameShark.Web.Services;

public class ApiClient : IApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<Paging<ProdutoDto>?> GetProdutosAsync(string? searchTerm, int? categoriaId, int? plataformaId, int page = 1)
    {
        var query = $"?page={page}&pageSize=12";
        if (!string.IsNullOrEmpty(searchTerm)) query += $"&searchTerm={searchTerm}";
        if (categoriaId.HasValue) query += $"&categoriaId={categoriaId}";
        if (plataformaId.HasValue) query += $"&plataformaId={plataformaId}";

        return await _httpClient.GetFromJsonAsync<Paging<ProdutoDto>>($"/api/produtos{query}");
    }

    public async Task<IEnumerable<CategoriaDto>?> GetCategoriasAsync() =>
        await _httpClient.GetFromJsonAsync<IEnumerable<CategoriaDto>>("/api/categorias");

    public async Task<IEnumerable<PlataformaDto>?> GetPlataformasAsync() =>
        await _httpClient.GetFromJsonAsync<IEnumerable<PlataformaDto>>("/api/plataformas");
}