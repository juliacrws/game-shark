using Microsoft.AspNetCore.Identity;
using GameShark.Infrastructure.Entities;

namespace GameShark.Web.Data;

public static class RoleSeeder
{
    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // 1. A Nova Hierarquia da GameShark
        string[] roles = { "Admin", "Gerente", "Estoquista", "Player" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // 2. Contratação Automática da Equipe (O Seed dos Usuários)
        // Conta do Dono (Acesso Absoluto)
        await CriarUsuarioBase(userManager, "admin@gameshark.com", "Admin@123", "Boss", "Admin");

        // Conta do Gerente (Vê faturamento, mas não deleta o banco)
        await CriarUsuarioBase(userManager, "gerente@gameshark.com", "Gerente@123", "Gerente de Vendas", "Gerente");

        // Conta do Estoquista (Só mexe em quantidade de produtos)
        await CriarUsuarioBase(userManager, "estoque@gameshark.com", "Estoque@123", "Mestre das Caixas", "Estoquista");
    }

    // 👇 O MÉTODO MÁGICO QUE FAZ O TRABALHO SUJO DE CRIAR AS CONTAS
    private static async Task CriarUsuarioBase(UserManager<ApplicationUser> userManager, string email, string senha, string apelido, string role)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            var newUser = new ApplicationUser
            {
                UserName = email,
                Email = email,
                NomeCompleto = apelido,
                Apelido = apelido,
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(newUser, senha);

            if (createResult.Succeeded)
            {
                await userManager.AddToRoleAsync(newUser, role);
            }
        }
        else
        {
            // Se a conta já existir, mas perdeu o crachá, devolvemos pra ela
            if (!await userManager.IsInRoleAsync(user, role))
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}