using System.ComponentModel.DataAnnotations.Schema; // 👈 Adiciona este using
namespace GameShark.Domain.Entities;

public class PedidoItem
{
    public int Id { get; set; }

    public int PedidoId { get; set; }
    public Pedido? Pedido { get; set; }

    public int ProdutoId { get; set; }
    public Produto? Produto { get; set; }

    public int Quantidade { get; set; }

    // Guardamos o preço no momento da compra, caso o jogo mude de preço no futuro
    public decimal PrecoUnitario { get; set; }
}