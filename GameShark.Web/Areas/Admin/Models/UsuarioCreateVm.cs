using System.ComponentModel.DataAnnotations;

namespace GameShark.Web.Areas.Admin.Models;

public class UsuarioCreateVm
{
    [Required(ErrorMessage = "Nome Completo é obrigatório.")]
    public string NomeCompleto { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A Senha inicial é obrigatória.")]
    [DataType(DataType.Password)]
    public string Senha { get; set; } = string.Empty;

    [Required(ErrorMessage = "Selecione um Cargo para o usuário.")]
    public string Cargo { get; set; } = string.Empty;
}