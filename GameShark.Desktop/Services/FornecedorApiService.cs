using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GameShark.Desktop.Services
{
    // DTO para transportar os dados
    public class FornecedorDto
    {
        public int Id { get; set; }
        public string NomeFantasia { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class FornecedorApiService
    {
        private readonly HttpClient _httpClient;

        // Injetando o HttpClient igual você deve ter feito nos outros services
        public FornecedorApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // READ - Listar todos
        public async Task<List<FornecedorDto>> ObterTodosAsync()
        {
            var resultado = await _httpClient.GetFromJsonAsync<List<FornecedorDto>>("api/fornecedores");
            return resultado ?? new List<FornecedorDto>();
        }

        // CREATE - Criar novo
        public async Task<bool> CriarAsync(FornecedorDto fornecedor)
        {
            var response = await _httpClient.PostAsJsonAsync("api/fornecedores", fornecedor);
            return response.IsSuccessStatusCode;
        }

        // UPDATE - Atualizar existente
        public async Task<bool> AtualizarAsync(int id, FornecedorDto fornecedor)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/fornecedores/{id}", fornecedor);
            return response.IsSuccessStatusCode;
        }

        // DELETE - Excluir
        public async Task<bool> DeletarAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/fornecedores/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}