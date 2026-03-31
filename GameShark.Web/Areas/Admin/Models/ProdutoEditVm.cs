using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http; // Necessário para o IFormFile
using GameShark.Domain.Enums; // 👈 Adicionado para acessar as idades

namespace GameShark.Web.Areas.Admin.Models;

public class ProdutoEditVm
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do loot é obrigatório.")]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Defina um preço.")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "Estoque não pode ser negativo.")]
    public int Estoque { get; set; }

    // Mantém a URL caso o usuário não envie uma nova imagem na edição
    public string? ImagemUrl { get; set; }

    [Display(Name = "Arte do Jogo/Funko")]
    public IFormFile? Capa { get; set; } // Propriedade mágica para o Upload

    [Required(ErrorMessage = "Selecione a categoria.")]
    public int CategoriaId { get; set; }

    [Required(ErrorMessage = "Selecione a plataforma.")]
    public int PlataformaId { get; set; }

    // 👇 O CAMPO NOVO QUE FALTAVA
    [Required(ErrorMessage = "Defina a faixa etária do jogo.")]
    [Display(Name = "Classificação Indicativa")]
    public ClassificacaoIndicativa Classificacao { get; set; }
}