namespace GameShark.Domain.Entities;
using GameShark.Domain.Enums; // Adicione o using

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
    public string ImagemUrl { get; set; } = string.Empty;

    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    public int PlataformaId { get; set; }
    public Plataforma? Plataforma { get; set; }
    public ClassificacaoIndicativa Classificacao { get; set; }
}