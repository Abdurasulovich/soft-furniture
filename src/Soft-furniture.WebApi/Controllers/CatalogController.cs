using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.Service.Dtos.Catalogs;
using Soft_furniture.Service.Interfaces.Catalogs;
using Soft_furniture.Service.Validators.Dtos.Catalogs;

namespace Soft_furniture.WebApi.Controllers;

[Route("api/catalogs")]
[ApiController]
public class CatalogController : ControllerBase
{
    private ICatalogService _service;
    private readonly int maxPageSize = 30;
    public CatalogController(ICatalogService service)
    {
        this._service = service;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{catalogId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAsync(long catalogId)
        => Ok(await _service.GetByIdAsync(catalogId));

    [HttpGet("count")]
    [AllowAnonymous]
    public async Task<IActionResult> CountAsync()
        => Ok(await _service.CountAsync());

    [HttpPost]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> CreateAsync([FromForm] CatalogCreateDto dto)
    {
        var createValidator = new CatalogCreateValidator();
        var result = createValidator.Validate(dto);

        if (result.IsValid) return Ok(await _service.CreateAsync(dto));
        else
        {
            return BadRequest(result.Errors);
        }
    }

    [HttpPut("{catalogId}")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> UpdateAsync(long catalogId, [FromForm] CatalogUpdateDto dto)
    {
        var updateValidator = new CatalogUpdateValidator();
        var validationResult = updateValidator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _service.UpdateAsync(catalogId, dto));
        else return BadRequest(validationResult.Errors);
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> DeleteAsync(long catalogId)
        => Ok(await _service.DeleteAsync(catalogId));

    //#region Products

    //[HttpGet("{catalogId}/products")]
    //public async Task<IActionResult> GetProductsByCatalodIdAsync(long catalogId, [FromQuery] PaginationParams @params)
    //    => Ok(catalogId); 
    //#endregion


}
