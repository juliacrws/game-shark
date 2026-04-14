using System.Net.Http;

namespace GameShark.Desktop.Services;

public class ApiClientService
{
    public readonly HttpClient _httpClient;

    // A porta oficial da GameShark.Api que validamos!
    public const string ApiBaseUrl = "http://localhost:5273";

    public ApiClientService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new System.Uri(ApiBaseUrl);

        // Futuramente, se a API exigir Token de Login, ele será configurado aqui.
    }
}