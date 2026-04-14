using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GameShark.Desktop.Services;

public class AuthApiService
{
    private readonly HttpClient _http;

    public AuthApiService()
    {
        _http = new ApiClientService()._httpClient;
    }

    public async Task<AuthResponseModel?> FazerLoginAsync(string email, string senha)
    {
        var payload = new { Email = email, Senha = senha };
        var response = await _http.PostAsJsonAsync("/api/auth/login", payload);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<AuthResponseModel>();
        }

        return null; // Retorna nulo se o login falhar
    }
}

public class AuthResponseModel
{
    public string Token { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}