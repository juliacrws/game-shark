using System.ComponentModel.DataAnnotations;

namespace GameShark.Web.Models;

public class RegisterVm
{
    [Required(ErrorMessage = "O E-mail é vital para o seu registro.")]
    [EmailAddress(ErrorMessage = "E-mail inválido, recruta.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Qual é o seu nome?")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A Senha de acesso é obrigatória.")]
    [DataType(DataType.Password)]
    public string Senha { get; set; } = string.Empty;

    // 👇 A propriedade que vai validar se as duas senhas digitadas são iguais
    [Required(ErrorMessage = "Você precisa confirmar a senha.")]
    [DataType(DataType.Password)]
    [Compare("Senha", ErrorMessage = "As senhas não são idênticas. Tente novamente.")]
    [Display(Name = "Confirmar Senha")]
    public string ConfirmarSenha { get; set; } = string.Empty;
}