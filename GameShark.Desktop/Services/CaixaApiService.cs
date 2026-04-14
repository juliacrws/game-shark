using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GameShark.Desktop.Services;

public class CaixaApiService
{
    private readonly HttpClient _http;

    public CaixaApiService()
    {
        // Pega a base configurada no ApiClientService
        _http = new ApiClientService()._httpClient;
    }

    // Exemplo de chamada que faremos para abrir o caixa
    public async Task<bool> AbrirCaixaAsync(decimal valorInicial)
    {
        var response = await _http.PostAsJsonAsync("/api/caixa/abrir", new { Valor = valorInicial });
        return response.IsSuccessStatusCode;
    }
}