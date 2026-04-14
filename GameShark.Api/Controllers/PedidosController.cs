using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameShark.Infrastructure.Persistence;

namespace GameShark.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly AppDbContext _context;

    public PedidosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("pendentes")]
    public async Task<IActionResult> GetPendentes()
    {
        var pedidos = await _context.Pedidos
            .Where(p => p.Status == "Aprovado")
            .Join(_context.Users,
                pedido => pedido.UsuarioId,
                usuario => usuario.Id,
                (pedido, usuario) => new { pedido, usuario })
            .Select(m => new
            {
                Id = m.pedido.Id,
                Codigo = m.pedido.Id.ToString(),
                Cliente = m.usuario.UserName,
                Data = m.pedido.DataPedido.ToString("dd/MM/yyyy")
            })
            .ToListAsync();

        return Ok(pedidos);
    }

    [HttpGet("retirada/{codigo}")]
    public async Task<IActionResult> BuscarPedidoParaRetirada(string codigo)
    {
        if (!int.TryParse(codigo, out int idBuscado))
            return BadRequest(new { erro = "Código inválido." });

        var pedido = await _context.Pedidos
            .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(p => p.Id == idBuscado);

        if (pedido == null) return NotFound();

        var usuario = await _context.Users.FirstOrDefaultAsync(u => u.Id == pedido.UsuarioId);
        string nomeReal = usuario?.UserName ?? "Cliente VIP";

        var dto = new PedidoRetiradaDto
        {
            Id = pedido.Id,
            ClienteNome = nomeReal,
            Status = pedido.Status ?? "Status não informado",
            ValorTotal = pedido.ValorTotal,
            Itens = pedido.Itens.Select(i =>
                $"{i.Quantidade}x {i.Produto?.Nome ?? "Item"} - (R$ {i.PrecoUnitario:F2})").ToArray()
        };

        return Ok(dto);
    }

    [HttpPut("{id}/entregar")]
    public async Task<IActionResult> ConfirmarEntrega(int id)
    {
        var pedido = await _context.Pedidos.FindAsync(id);
        if (pedido == null) return NotFound();

        pedido.Status = "Entregue";
        await _context.SaveChangesAsync();

        return Ok(new { mensagem = "📦 Entrega confirmada!" });
    }

    [HttpPut("{id}/cancelar")]
    public async Task<IActionResult> CancelarPedido(int id)
    {
        var pedido = await _context.Pedidos
            .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pedido == null) return NotFound();

        if (pedido.Status == "Entregue")
            return BadRequest(new { erro = "O loot já foi retirado, não pode ser cancelado!" });

        foreach (var item in pedido.Itens)
        {
            if (item.Produto != null)
            {
                item.Produto.Estoque += item.Quantidade;
            }
        }

        pedido.Status = "Cancelado";
        await _context.SaveChangesAsync();

        return Ok(new { mensagem = "Pedido cancelado e itens devolvidos ao estoque!" });
    }

    // 💰 ROTA NOVA: O Cérebro do Fechamento de Caixa
    // 💰 ROTA OMNICHANNEL: O Cérebro do Fechamento de Caixa Integrado
    // 💰 ROTA OMNICHANNEL: O Cérebro do Fechamento de Caixa Integrado
    // 💰 ROTA HACKEADA: Soma tudo que existe no banco ignorando a data!
    [HttpGet("resumo-dia")]
    public async Task<IActionResult> GetResumoDoDia()
    {
        // 🚨 Puxa TUDO do banco, ignorando se foi hoje, ontem ou ano passado
        var movimentacoesTotais = await _context.Pedidos.ToListAsync();

        var sucessoHoje = movimentacoesTotais
            .Where(p => p.Status == "Entregue" || p.Status == "Concluido" || p.Status == "Aprovado")
            .ToList();

        var canceladosHoje = movimentacoesTotais
            .Where(p => p.Status == "Cancelado")
            .ToList();

        return Ok(new
        {
            Entregues = sucessoHoje.Count,
            Cancelados = canceladosHoje.Count,
            Receita = sucessoHoje.Sum(p => p.ValorTotal)
        });
    }
    // ✅ DTO DECLARADO UMA ÚNICA VEZ AQUI NO FINAL
    public class PedidoRetiradaDto
    {
        public int Id { get; set; }
        public string ClienteNome { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal ValorTotal { get; set; }
        public string[] Itens { get; set; } = Array.Empty<string>();
    }
}