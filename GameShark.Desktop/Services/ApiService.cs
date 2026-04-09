using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GameShark.Desktop.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        // 👇 Essa mágica ignora erros de certificado SSL no seu computador
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

        _httpClient = new HttpClient(handler);
        _httpClient.BaseAddress = new System.Uri("https://localhost:7096");
    }

    // Exemplo de método para testarmos a comunicação: Buscar o Catálogo!
    // OBS: Como referenciamos o GameShark.Application, podemos usar os DTOs reais aqui se existirem,
    // mas para o teste inicial, vamos ler como uma lista de objetos anônimos ou dinâmicos.
    public async Task<string> TestarConexaoAsync()
    {
        try
        {
            // Vamos bater em uma rota pública da sua API para testar
            var response = await _httpClient.GetAsync("/api/produtos"); // Ajuste para a rota real de produtos da sua API

            if (response.IsSuccessStatusCode)
            {
                return "🟢 SUCESSO! Conectado à API da GameShark Central.";
            }
            return $"🔴 ERRO DA API: {response.StatusCode}";
        }
        catch (System.Exception ex)
        {
            return $"🔴 ERRO DE REDE: API desligada ou porta errada. ({ex.Message})";
        }
    }
}