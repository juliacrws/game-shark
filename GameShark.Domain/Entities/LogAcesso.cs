using System.ComponentModel.DataAnnotations;

namespace GameShark.Domain.Entities; // Ajuste se o seu namespace for diferente

public class LogAcesso
{
    [Key]
    public int Id { get; set; }
    public string EmailTentativa { get; set; } = string.Empty;
    public DateTime DataHora { get; set; }
    public bool Sucesso { get; set; }
    public string IpAddress { get; set; } = string.Empty;
}