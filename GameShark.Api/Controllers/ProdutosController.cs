using GameShark.Application.Filters;
using GameShark.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameShark.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoQueryService _produtoQueryService;

    // Construtor limpo e legível
    public ProdutosController(IProdutoQueryService produtoQueryService)
    {
        _produtoQueryService = produtoQueryService;
    }

    // 🌐 ENDPOINT PRINCIPAL: Retorna o catálogo paginado (Usado pelo Site e pelo PDV)
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] ProdutoFilter filter)
    {
        var result = await _produtoQueryService.GetCatalogAsync(filter);

        return Ok(result);
    }
}