using System.ComponentModel.DataAnnotations;

namespace GameShark.Web.Models;

public class MudarSenhaVm
{
    [Required(ErrorMessage = "A senha atual é obrigatória.")]
    [DataType(DataType.Password)]
    public string SenhaAtual { get; set; } = string.Empty;

    [Required(ErrorMessage = "A nova senha é obrigatória.")]
    [DataType(DataType.Password)]
    public string NovaSenha { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Compare("NovaSenha", ErrorMessage = "As senhas não coincidem. Tente novamente.")]
    public string ConfirmarSenha { get; set; } = string.Empty;
}