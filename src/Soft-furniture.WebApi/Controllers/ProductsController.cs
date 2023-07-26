using Microsoft.AspNetCore.Mvc;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.Domain.Entities.Products;
using Soft_furniture.Service.Dtos.Catalogs;
using Soft_furniture.Service.Dtos.Products;
using Soft_furniture.Service.Interfaces.Products;
using Soft_furniture.Service.Validators.Dtos.Catalogs;
using Soft_furniture.Service.Validators.Dtos.Products;

namespace Soft_furniture.WebApi.Controllers;



[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private IProductService _service;
    private readonly int maxPageSize = 30;

    public ProductsController(IProductService service)
    {
        this._service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllByTypeIdAsync([FromQuery] long typeId, int page = 1)
        => Ok(await _service.GetAllByTypeIdAsync(typeId, new PaginationParams(page, maxPageSize)));

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetByIdAsync(long productId)
        => Ok(await _service.GetByIdAsync(productId));

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
        => Ok(await _service.CountAsync());

    [HttpGet("Search")]
    public async Task<IActionResult> SearchAnsyc([FromQuery] string search, int page = 1)
    {
        var result= await _service.SearchAsync(search, new PaginationParams(page, maxPageSize));
        return Ok(result.Item2);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] ProductsCreateDto dto)
    {
        var createValidator = new ProductCreateValidator();
        var result = createValidator.Validate(dto);

        if (result.IsValid) return Ok(await _service.CreateAsync(dto));
        else
        {
            return BadRequest(result.Errors);
        }
    }

    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateAsync(long productId, [FromForm] ProductsUpdateDto dto)
    {
        var updateValidator = new ProductUpdateValidator();
        var validationResult = updateValidator.Validate(dto);
        if (validationResult.IsValid) return Ok(await _service.UpdateAsync(productId, dto));
        else return BadRequest(validationResult.Errors);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(long productId)
        => Ok(await _service.DeleteAsync(productId));
}
