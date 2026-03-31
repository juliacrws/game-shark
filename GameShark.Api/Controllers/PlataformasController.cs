using GameShark.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameShark.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlataformasController : ControllerBase
{
    private readonly ICatalogLookupService _lookupService;
    public PlataformasController(ICatalogLookupService lookupService) => _lookupService = lookupService;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _lookupService.GetPlataformasAsync());
}