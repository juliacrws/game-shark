using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GameShark.Desktop.Services;

public class VendaApiService
{
    private readonly HttpClient _http;

    public VendaApiService()
    {
        _http = new ApiClientService()._httpClient;
    }

    // 🚀 BUSCA REAL NO BANCO DE DADOS: Bate no seu ProdutosController
    public async Task<List<ProdutoVendaModel>> ObterCatalogoDaApiAsync()
    {
        try
        {
            // 1. Lemos o "envelope" inteiro (A Paginação)
            var envelope = await _http.GetFromJsonAsync<PaginacaoResponseModel<ProdutoVendaModel>>("/api/produtos");

            // 2. Abrimos o envelope e devolvemos só a lista de itens!
            return envelope?.Items ?? new List<ProdutoVendaModel>();
        }
        catch (Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(
                $"Falha ao buscar os jogos.\n\nDetalhe técnico: {ex.Message}",
                "Radar GameShark",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Error);

            return new List<ProdutoVendaModel>();
        }
    }

    // 🚀 AGORA ELE PEDE O CARRINHO JUNTO COM O TOTAL
    public async Task<bool> FinalizarVendaAsync(decimal valorTotal, List<ItemCarrinhoModel> carrinho)
    {
        // 1. Converte o seu carrinho visual para o formato que a API entende
        var itensParaApi = carrinho.Select(item => new
        {
            ProdutoId = item.Produto.Id,
            Quantidade = item.Quantidade,
            PrecoUnitario = item.Produto.Preco
        }).ToList();

        // 2. Coloca tudo no "envelope"
        var payload = new
        {
            Valor = valorTotal,
            Tipo = "Entrada",
            Descricao = "Venda Balcão PDV",
            Itens = itensParaApi // 👈 Mandando os itens!
        };

        var response = await _http.PostAsJsonAsync("/api/caixa/movimentacao", payload);
        return response.IsSuccessStatusCode;
    }
}

// ⚠️ ATENÇÃO AQUI: Os nomes (Id, Nome, Preco) precisam estar iguais aos 
// do ProdutoDto que a sua API devolve! (Se na API for "Valor" ou "Titulo", mude aqui).
public class ProdutoVendaModel
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
}

public class ItemCarrinhoModel
{
    public ProdutoVendaModel Produto { get; set; } = new();
    public int Quantidade { get; set; }
    public decimal Subtotal => Produto.Preco * Quantidade;
}
// Molde do envelope paginado da sua API
public class PaginacaoResponseModel<T>
{
    public int TotalItems { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }

    // 👇 É AQUI QUE OS JOGOS ESTÃO ESCONDIDOS!
    public List<T> Items { get; set; } = new List<T>();
}