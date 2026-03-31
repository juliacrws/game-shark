using GameShark.Domain.Entities;
using GameShark.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameShark.Infrastructure.Persistence.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        await context.Database.MigrateAsync();

        // 1. Roles
        string[] roles = { TipoUsuario.Admin, TipoUsuario.Gerente, TipoUsuario.Cliente };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // 2. Admin User
        if (await userManager.FindByEmailAsync("admin@gameshark.com") == null)
        {
            var admin = new ApplicationUser { UserName = "admin@gameshark.com", Email = "admin@gameshark.com", NomeCompleto = "Mestre Kame" };
            await userManager.CreateAsync(admin, "Admin@123");
            await userManager.AddToRoleAsync(admin, TipoUsuario.Admin);
        }

        // 3. Categorias Base
        if (!context.Categorias.Any())
        {
            context.Categorias.AddRange(
                new Categoria { Nome = "Sobrevivência e Terror", Descricao = "Clima tenso e adrenalina" },
                new Categoria { Nome = "Mundo Aberto", Descricao = "Liberdade para explorar" },
                new Categoria { Nome = "Ícones dos Games (Funkos)", Descricao = "Action figures perfeitos" }
            );
            await context.SaveChangesAsync();
        }

        // 4. Plataformas Base
        if (!context.Plataformas.Any())
        {
            context.Plataformas.AddRange(
                new Plataforma { Nome = "PlayStation 5" },
                new Plataforma { Nome = "PC" },
                new Plataforma { Nome = "Colecionável" }
            );
            await context.SaveChangesAsync();
        }

        // 5. Produtos Iniciais
        if (!context.Produtos.Any())
        {
            var catTerror = context.Categorias.First(c => c.Nome == "Sobrevivência e Terror").Id;
            var catFunkos = context.Categorias.First(c => c.Nome == "Ícones dos Games (Funkos)").Id;

            var platPS5 = context.Plataformas.First(p => p.Nome == "PlayStation 5").Id;
            var platCol = context.Plataformas.First(p => p.Nome == "Colecionável").Id;

            context.Produtos.AddRange(
                new Produto { Nome = "Resident Evil 4 Remake", Descricao = "O terror redefinido", Preco = 249.90m, Estoque = 15, CategoriaId = catTerror, PlataformaId = platPS5 },
                new Produto { Nome = "Funko Pop Ghostface", Descricao = "O clássico slasher de Pânico", Preco = 129.90m, Estoque = 5, CategoriaId = catFunkos, PlataformaId = platCol }
            );
            await context.SaveChangesAsync();
        }
    }
}