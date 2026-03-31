namespace GameShark.Web.Models;

public class CatalogViewModel
{
    public Paging<ProdutoDto> Produtos { get; set; } = new();
    public IEnumerable<CategoriaDto> Categorias { get; set; } = new List<CategoriaDto>();
    public IEnumerable<PlataformaDto> Plataformas { get; set; } = new List<PlataformaDto>();

    // Filtros selecionados
    public string? SearchTerm { get; set; }
    public int? CategoriaId { get; set; }
    public int? PlataformaId { get; set; }
}