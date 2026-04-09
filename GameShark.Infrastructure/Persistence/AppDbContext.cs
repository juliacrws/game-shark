using GameShark.Domain.Entities;
using GameShark.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameShark.Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<GameShark.Domain.Entities.Pedido> Pedidos { get; set; }
    public DbSet<GameShark.Domain.Entities.PedidoItem> PedidoItens { get; set; }
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<AlertaEstoque> AlertasEstoque { get; set; }
    public DbSet<Plataforma> Plataformas => Set<Plataforma>();
    public DbSet<GameShark.Domain.Entities.CarrinhoItem> CarrinhoItems { get; set; }
    public DbSet<LogAcesso> LogsAcesso { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Resolve o aviso do ValorTotal no Pedido
        builder.Entity<Pedido>()
            .Property(p => p.ValorTotal)
            .HasColumnType("decimal(18,2)");

        // Resolve o aviso do PrecoUnitario no PedidoItem
        builder.Entity<PedidoItem>()
            .Property(pi => pi.PrecoUnitario)
            .HasColumnType("decimal(18,2)");

        // Aproveite e faça o mesmo para o Preço do Produto se ainda não fez!
        builder.Entity<Produto>()
            .Property(p => p.Preco)
            .HasColumnType("decimal(18,2)");
    }
}