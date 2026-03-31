namespace GameShark.Application.Filters;

public class ProdutoFilter
{
    public string? SearchTerm { get; set; }
    public int? CategoriaId { get; set; }
    public int? PlataformaId { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}