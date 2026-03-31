using System.ComponentModel.DataAnnotations;

namespace GameShark.Web.Models;

public class LoginVm
{
    [Required(ErrorMessage = "O Email é obrigatório")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A Senha é obrigatória")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
}