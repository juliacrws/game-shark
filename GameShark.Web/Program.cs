using GameShark.Domain.Entities;
using GameShark.Infrastructure.Entities;
using GameShark.Infrastructure.Persistence;
using GameShark.Web.Services;
using GameShark.Web.Identity; // 👈 Adicionamos o using para o nosso tradutor
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// 1. Configura a chamada para a API
var apiBaseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");
builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl!);
});

// 2. Configura o Banco de Dados e o Identity para o Login
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddErrorDescriber<PortuguesIdentityErrorDescriber>(); // 👈 A MÁGICA ENTRA AQUI!

// 3. Configura o Cookie de Autenticação
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/AccessDenied";
});

// 4. Cria as Policies de Acesso
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole(TipoUsuario.Admin));
    options.AddPolicy("ManagerOrAdmin", policy => policy.RequireRole(TipoUsuario.Gerente, TipoUsuario.Admin));
});

builder.Services.AddScoped<GameShark.Web.Areas.Admin.Services.DashboardService>();

// 1. Configura a memória temporária do servidor (Necessário para a Sessão funcionar)
builder.Services.AddDistributedMemoryCache();

// 2. Configura a Sessão em si
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// A ordem aqui é VITAL: Autenticar primeiro, Autorizar depois.
app.UseAuthentication();
app.UseAuthorization();

// 👇 A Sessão OBRIGATORIAMENTE tem que ficar aqui! 
// (Depois do Routing/Auth e ANTES do MapControllerRoute)
app.UseSession();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


// 👇 CÓDIGO NOVO AQUI: Inicializa os cargos no banco antes de rodar o app
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // O C# vai pedir para você importar o namespace do RoleSeeder, 
        // ou você pode colocar "using GameShark.Web.Data;" lá no topo!
        await GameShark.Web.Data.RoleSeeder.SeedRolesAsync(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Um erro ocorreu ao injetar os cargos no banco de dados.");
    }
}
app.Run();