using System.Security.Claims;
using GameShark.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShark.Web.Controllers;

[Authorize] // 👈 Apenas jogadores com login podem ver os seus pedidos
public class PedidosController : Controller
{
    private readonly AppDbContext _context;

    public PedidosController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // 1. Descobrir quem é o utilizador logado
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // 2. Ir à base de dados buscar os pedidos dele, incluindo os jogos lá dentro
        var meusPedidos = await _context.Pedidos
            .Include(p => p.Itens)
                .ThenInclude(i => i.Produto) // 👈 O "ThenInclude" serve para entrar na lista de itens e puxar a foto/nome do jogo
            .Where(p => p.UsuarioId == userId)
            .OrderByDescending(p => p.DataPedido) // Os mais recentes primeiro
            .ToListAsync();

        return View(meusPedidos);
    }
}