using GameShark.Domain.Entities; // Ajuste se a sua entidade Pedido estiver em outro namespace

namespace GameShark.Web.Areas.Admin.Models;

public class CaixaVm
{
    public decimal FaturamentoTotal { get; set; }
    public decimal FaturamentoMes { get; set; }
    public decimal FaturamentoHoje { get; set; }
    public List<Pedido> UltimasVendas { get; set; } = new();
}