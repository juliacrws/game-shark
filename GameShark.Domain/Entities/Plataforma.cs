namespace GameShark.Domain.Entities;

public class Plataforma
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty; // ex: PS5, PC, BoardGame, Colecionável

    public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}