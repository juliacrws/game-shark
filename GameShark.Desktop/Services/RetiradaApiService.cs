using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms; // 👈 Necessário para exibir alertas de erro direto do serviço

namespace GameShark.Desktop.Services;

public class RetiradaApiService
{
    private readonly HttpClient _http;

    public RetiradaApiService()
    {
        // 💡 Utiliza o HttpClient configurado no seu ApiClientService
        _http = new ApiClientService()._httpClient;
    }

    // 🚀 BUSCA A LISTA COMPLETA: Para preencher o Grid (Tabela)
    public async Task<List<PedidoResumoModel>> ObterPendentesAsync()
    {
        try
        {
            var response = await _http.GetFromJsonAsync<List<PedidoResumoModel>>("/api/pedidos/pendentes");
            return response ?? new List<PedidoResumoModel>();
        }
        catch (Exception)
        {
            // Em caso de erro, retorna uma lista vazia para não quebrar a UI
            return new List<PedidoResumoModel>();
        }
    }

    // 🔍 BUSCA INDIVIDUAL: Para carregar o "Card" de detalhes
    public async Task<PedidoRetiradaModel?> BuscarPedidoAsync(string codigo)
    {
        try
        {
            var response = await _http.GetAsync($"/api/pedidos/retirada/{codigo}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PedidoRetiradaModel>();
            }

            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    // ✅ CONFIRMAÇÃO: Para dar a baixa da entrega na Matriz
    public async Task<bool> ConfirmarEntregaAsync(int idPedido)
    {
        try
        {
            var response = await _http.PutAsync($"/api/pedidos/{idPedido}/entregar", null);
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }

    // ❌ CANCELAMENTO E DEVOLUÇÃO: Devolve os itens ao estoque!
    public async Task<bool> CancelarPedidoAsync(int idPedido)
    {
        try
        {
            var response = await _http.PutAsync($"/api/pedidos/{idPedido}/cancelar", null);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Conexão perdida com a Matriz: {ex.Message}", "Radar GameShark");
            return false;
        }
    }

    // 💰 RESUMO DO DIA: Para a Dashboard de Fechamento de Caixa!
    public async Task<ResumoDiaModel> ObterResumoDoDiaAsync()
    {
        try
        {
            var response = await _http.GetFromJsonAsync<ResumoDiaModel>("/api/pedidos/resumo-dia");
            return response ?? new ResumoDiaModel();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao carregar o fechamento: {ex.Message}", "Radar GameShark");
            return new ResumoDiaModel(); // Retorna zerado se der erro para não travar a tela
        }
    }
}

// --- 📦 MODELS (DTOs) ---
public class PedidoRetiradaModel
{
    public int Id { get; set; }
    public string ClienteNome { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal ValorTotal { get; set; }
    public string[] Itens { get; set; } = Array.Empty<string>();
}

public class PedidoResumoModel
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Cliente { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
}

public class ResumoDiaModel
{
    public int Entregues { get; set; }
    public int Cancelados { get; set; }
    public decimal Receita { get; set; }
}