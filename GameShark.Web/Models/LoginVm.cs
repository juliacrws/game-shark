using System.ComponentModel.DataAnnotations;

namespace GameShark.Web.Models;

public class LoginVm
{
    [Required(ErrorMessage = "O E-mail é vital para o login.")]
    [EmailAddress(ErrorMessage = "E-mail inválido, recruta.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A Senha de acesso é obrigatória.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
}