using GameShark.Application.Filters;
using GameShark.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameShark.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoQueryService _produtoQueryService;
    public ProdutosController(IProdutoQueryService produtoQueryService) => _produtoQueryService = produtoQueryService;

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] ProdutoFilter filter)
    {
        var result = await _produtoQueryService.GetCatalogAsync(filter);
        return Ok(result);
    }
}