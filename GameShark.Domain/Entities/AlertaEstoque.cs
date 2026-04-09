using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameShark.Domain.Entities;

public class AlertaEstoque
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string EmailCliente { get; set; } = string.Empty;

    public int ProdutoId { get; set; }

    [ForeignKey("ProdutoId")]
    public Produto? Produto { get; set; }

    public DateTime DataSolicitacao { get; set; } = DateTime.Now;

    // Para você saber no futuro se já mandou o e-mail ou não
    public bool StatusResolvido { get; set; } = false;
}