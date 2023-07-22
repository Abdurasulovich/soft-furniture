using Microsoft.AspNetCore.Mvc;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.Domain.Entities.Furniture_Catalog;
using Soft_furniture.Service.Dtos.Catalogs;
using Soft_furniture.Service.Interfaces.Catalogs;
using Soft_furniture.Service.Validators.Dtos.Catalogs;
using System.Diagnostics.CodeAnalysis;

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
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{catalogId}")]
    public async Task<IActionResult> GetByIdAsync(long catalogId)
        => Ok(await _service.GetByIdAsync(catalogId)); 

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _service.CountAsync());

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] CatalogCreateDto dto)
    {
        var createValidator = new CatalogCreateValidator();
        var result = createValidator.Validate(dto);

        if(result.IsValid) return Ok(await _service.CreateAsync(dto));
        else
        {
            return BadRequest(result.Errors);
        }
    }

    [HttpPut("{catalogId}")]
    public async Task<IActionResult> UpdateAsync(long catalogId, [FromForm] CatalogUpdateDto dto)
    {
        var updateValidator = new CatalogUpdateValidator();
        var validationResult = updateValidator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _service.UpdateAsync(catalogId, dto));
        else return BadRequest(validationResult.Errors);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(long catalogId)
        => Ok(await _service.DeleteAsync(catalogId));

    //#region Products

    //[HttpGet("{catalogId}/products")]
    //public async Task<IActionResult> GetProductsByCatalodIdAsync(long catalogId, [FromQuery] PaginationParams @params)
    //    => Ok(catalogId); 
    //#endregion

    
}
