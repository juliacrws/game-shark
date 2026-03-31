using System;
using Microsoft.AspNetCore.Identity;

namespace GameShark.Infrastructure.Entities; // 👈 A MÁGICA ESTÁ AQUI!

public class ApplicationUser : IdentityUser
{
    public string? NomeCompleto { get; set; }
    public string? FotoPerfilUrl { get; set; }
    public string? Bio { get; set; }
    public DateTime? DataNascimento { get; set; }
    public string? Apelido { get; set; }
}