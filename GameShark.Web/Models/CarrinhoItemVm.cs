namespace GameShark.Web.Models;

public class CarrinhoItemVm
{
    public int ProdutoId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public string ImagemUrl { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public string Plataforma { get; set; } = string.Empty;

    public decimal SubTotal => Preco * Quantidade;

    // 👇 A LINHA QUE O COMPILADOR TÁ EXIGINDO
    public int EstoqueDisponivel { get; set; }
}