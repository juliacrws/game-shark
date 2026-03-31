using GameShark.Infrastructure.Entities;
using GameShark.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GameShark.Application.Abstractions.Repositories;
using GameShark.Application.Services;
using GameShark.Infrastructure.Repositories;
using GameShark.Infrastructure.Services;
namespace GameShark.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        // Repositories
        services.AddScoped<ICategoriaRepository, EfCategoriaRepository>();
        services.AddScoped<IPlataformaRepository, EfPlataformaRepository>();
        services.AddScoped<IProdutoRepository, EfProdutoRepository>();

        // Services
        services.AddScoped<ICatalogLookupService, CatalogLookupService>();
        services.AddScoped<IProdutoQueryService, ProdutoQueryService>();
        return services;
    }
}