using System.ComponentModel.DataAnnotations.Schema; // 👈 Adiciona este using
namespace GameShark.Domain.Entities;

public class Pedido
{
    public int Id { get; set; }

    // Vincula o pedido ao Utilizador Logado
    public string UsuarioId { get; set; } = string.Empty;

    public DateTime DataPedido { get; set; } = DateTime.Now;

    public decimal ValorTotal { get; set; }

    // Pode ser: "Aguardando Pagamento", "Aprovado", "Enviado"
    public string Status { get; set; } = "Aguardando Pagamento";

    public List<PedidoItem> Itens { get; set; } = new();
}