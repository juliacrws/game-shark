using GameShark.Domain.Entities;
using GameShark.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GameShark.Infrastructure.Persistence.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Aplica as migrations pendentes automaticamente
        await context.Database.MigrateAsync();

        // 1. Roles (Adicionamos o "PLAYER" aqui para corrigir o erro de cadastro!)
        string[] roles = { TipoUsuario.Admin, TipoUsuario.Gerente, TipoUsuario.Cliente, "PLAYER" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // 2. Usuários Iniciais
        // Admin
        if (await userManager.FindByEmailAsync("admin@gameshark.com") == null)
        {
            var admin = new ApplicationUser { UserName = "admin@gameshark.com", Email = "admin@gameshark.com", NomeCompleto = "Mestre Kame" };
            await userManager.CreateAsync(admin, "Admin@123");
            await userManager.AddToRoleAsync(admin, TipoUsuario.Admin);
        }

        // Gerente
        if (await userManager.FindByEmailAsync("gerente@gameshark.com") == null)
        {
            var gerente = new ApplicationUser { UserName = "gerente@gameshark.com", Email = "gerente@gameshark.com", NomeCompleto = "Mario Mario" };
            await userManager.CreateAsync(gerente, "Gerente@123");
            await userManager.AddToRoleAsync(gerente, TipoUsuario.Gerente);
        }

        // Cliente
        if (await userManager.FindByEmailAsync("cliente@gameshark.com") == null)
        {
            var cliente = new ApplicationUser { UserName = "cliente@gameshark.com", Email = "cliente@gameshark.com", NomeCompleto = "Lara Croft" };
            await userManager.CreateAsync(cliente, "Cliente@123");
            // Se "Cliente" e "PLAYER" forem a mesma coisa no seu projeto, você pode unificar depois.
            await userManager.AddToRoleAsync(cliente, TipoUsuario.Cliente);
        }

        // 3. Categorias Base
        if (!context.Categorias.Any())
        {
            context.Categorias.AddRange(
                new Categoria { Nome = "Sobrevivência e Terror", Descricao = "Clima tenso e muita adrenalina" },
                new Categoria { Nome = "Mundo Aberto", Descricao = "Liberdade total para explorar" },
                new Categoria { Nome = "Ícones dos Games (Funkos)", Descricao = "Action figures perfeitos para sua estante" },
                new Categoria { Nome = "RPG", Descricao = "Evolua seu personagem e viva histórias épicas" },
                new Categoria { Nome = "Ação e Aventura", Descricao = "Combates frenéticos e exploração" },
                new Categoria { Nome = "Esportes e Corrida", Descricao = "Competição em alto nível" }
            );
            await context.SaveChangesAsync();
        }

        // 4. Plataformas Base
        if (!context.Plataformas.Any())
        {
            context.Plataformas.AddRange(
                new Plataforma { Nome = "PlayStation 5" },
                new Plataforma { Nome = "PlayStation 4" },
                new Plataforma { Nome = "PC" },
                new Plataforma { Nome = "Xbox Series X/S" },
                new Plataforma { Nome = "Nintendo Switch" },
                new Plataforma { Nome = "Colecionável" }
            );
            await context.SaveChangesAsync();
        }

        // 5. Produtos Iniciais
        if (!context.Produtos.Any())
        {
            // Buscando os IDs para relacionar
            var catTerror = context.Categorias.First(c => c.Nome == "Sobrevivência e Terror").Id;
            var catFunkos = context.Categorias.First(c => c.Nome == "Ícones dos Games (Funkos)").Id;
            var catMundoAberto = context.Categorias.First(c => c.Nome == "Mundo Aberto").Id;
            var catRpg = context.Categorias.First(c => c.Nome == "RPG").Id;
            var catAcao = context.Categorias.First(c => c.Nome == "Ação e Aventura").Id;

            var platPS5 = context.Plataformas.First(p => p.Nome == "PlayStation 5").Id;
            var platPC = context.Plataformas.First(p => p.Nome == "PC").Id;
            var platXbox = context.Plataformas.First(p => p.Nome == "Xbox Series X/S").Id;
            var platSwitch = context.Plataformas.First(p => p.Nome == "Nintendo Switch").Id;
            var platCol = context.Plataformas.First(p => p.Nome == "Colecionável").Id;

            context.Produtos.AddRange(
                // Jogos de Terror
                new Produto { Nome = "Resident Evil 4 Remake", Descricao = "O terror redefinido, agora com gráficos de nova geração.", Preco = 249.90m, Estoque = 15, CategoriaId = catTerror, PlataformaId = platPS5 },
                new Produto { Nome = "Dead Space Remake", Descricao = "No espaço, ninguém pode ouvir você gritar.", Preco = 229.90m, Estoque = 8, CategoriaId = catTerror, PlataformaId = platXbox },

                // Mundo Aberto / RPG
                new Produto { Nome = "The Witcher 3: Wild Hunt", Descricao = "A obra-prima da CD Projekt Red.", Preco = 99.90m, Estoque = 30, CategoriaId = catMundoAberto, PlataformaId = platPC },
                new Produto { Nome = "Elden Ring", Descricao = "O Jogo do Ano de 2022. Prepare-se para morrer.", Preco = 299.90m, Estoque = 12, CategoriaId = catRpg, PlataformaId = platPS5 },
                new Produto { Nome = "Cyberpunk 2077", Descricao = "Bem-vindo a Night City.", Preco = 149.90m, Estoque = 20, CategoriaId = catRpg, PlataformaId = platPC },

                // Ação e Aventura
                new Produto { Nome = "God of War Ragnarök", Descricao = "O fim dos tempos na mitologia nórdica.", Preco = 279.90m, Estoque = 25, CategoriaId = catAcao, PlataformaId = platPS5 },
                new Produto { Nome = "The Legend of Zelda: Tears of the Kingdom", Descricao = "A sequência do aclamado Breath of the Wild.", Preco = 349.90m, Estoque = 10, CategoriaId = catAcao, PlataformaId = platSwitch },

                // Colecionáveis (Funkos)
                new Produto { Nome = "Funko Pop Ghostface", Descricao = "O clássico slasher de Pânico.", Preco = 129.90m, Estoque = 5, CategoriaId = catFunkos, PlataformaId = platCol },
                new Produto { Nome = "Funko Pop Kratos (com Machado Leviatã)", Descricao = "O Deus da Guerra direto para sua mesa.", Preco = 149.90m, Estoque = 3, CategoriaId = catFunkos, PlataformaId = platCol },
                new Produto { Nome = "Funko Pop Geralt de Rívia", Descricao = "O Bruxo pronto para caçar monstros (e enfeitar seu quarto).", Preco = 139.90m, Estoque = 7, CategoriaId = catFunkos, PlataformaId = platCol }
            );

            await context.SaveChangesAsync();
        }
    }
}