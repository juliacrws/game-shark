using GameShark.Infrastructure;
using GameShark.Infrastructure.Persistence.Seed;
using Scalar.AspNetCore; // Novo using para a interface visual

var builder = WebApplication.CreateBuilder(args);

// 1. Adiciona suporte a Controllers e o novo OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi(); // Substitui o AddSwaggerGen e AddEndpointsApiExplorer

// 2. Configura o CORS lendo do appsettings
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWeb", policy => policy
        .WithOrigins(allowedOrigins)
        .AllowAnyHeader()
        .AllowAnyMethod());
});

// 3. Injeção da Infraestrutura (Banco e Repositórios)
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// 4. Executa o Seed ao iniciar a API
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try { await DatabaseSeeder.SeedAsync(services); }
    catch (Exception ex) { Console.WriteLine($"Erro ao rodar o seed: {ex.Message}"); }
}

// 5. Middlewares HTTP
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Mapeia o documento JSON
    app.MapScalarApiReference(); // Cria a interface visual em /scalar/v1
}

app.UseCors("AllowWeb");
app.MapControllers();

app.Run();