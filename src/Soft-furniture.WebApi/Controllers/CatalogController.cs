using Microsoft.AspNetCore.Mvc;
using Soft_furniture.Service.Dtos.Catalogs;
using Soft_furniture.Service.Interfaces.Catalogs;

namespace Soft_furniture.WebApi.Controllers;

[Route("api/catalogs")]
[ApiController]
public class CatalogController : ControllerBase
{
    private ICatalogService _service;

    public CatalogController(ICatalogService service)
    {
        this._service = service;
    }
    [HttpGet]
    public async Task<IActionResult> CountAsync()
        => Ok(await _service.CountAsync());

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] CatalogCreateDto dto)
        => Ok(await _service.CreateAsync(dto));

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(long catalogId)
        => Ok(await _service.DeleteAsync(catalogId));
}
