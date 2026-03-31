namespace GameShark.Domain.Entities;

public class CarrinhoItem
{
    public int Id { get; set; }

    // Vincula o item ao Usuário Logado (Identity usa string para os IDs)
    public string UsuarioId { get; set; } = string.Empty;

    // Vincula o item ao Produto
    public int ProdutoId { get; set; }
    public Produto? Produto { get; set; }

    public int Quantidade { get; set; }
}