namespace GameShark.Web.Models;

public class ProdutoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public string ImagemUrl { get; set; } = string.Empty;
    public string CategoriaNome { get; set; } = string.Empty;
    public string PlataformaNome { get; set; } = string.Empty;
}

public class CategoriaDto { public int Id { get; set; } public string Nome { get; set; } = string.Empty; }
public class PlataformaDto { public int Id { get; set; } public string Nome { get; set; } = string.Empty; }

public class Paging<T>
{
    public int TotalItems { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<T> Items { get; set; } = new List<T>();
}