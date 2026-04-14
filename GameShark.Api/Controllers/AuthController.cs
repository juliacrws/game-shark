using Microsoft.AspNetCore.Mvc;

namespace GameShark.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto request)
    {
        // 💡 FUTURO: Fazer SELECT no banco procurando o Email e a Senha criptografada
        if (request.Email == "admin@gameshark.com" && request.Senha == "Admin@123")
        {
            // O Token é a "credencial" que usaremos no futuro para as rotas bloqueadas
            return Ok(new { Token = "mock-jwt-token", Nome = "Mestre Kame (Admin)", Role = "Mestre Supremo" });
        }

        return Unauthorized(new { erro = "E-mail ou senha inválidos. Acesso negado." });
    }
}

public class LoginDto 
{ 
    public string Email { get; set; } = string.Empty; 
    public string Senha { get; set; } = string.Empty; 
}