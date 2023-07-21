using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.Service.Dtos.Catalogs;
using Soft_furniture.Service.Dtos.Types;
using Soft_furniture.Service.Interfaces.Catalogs;

namespace Soft_furniture.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private ITypeSer _service;
        private readonly int maxPageSize = 30;
        public TypeController(ICatalogService service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

        [HttpGet("{TypeId}")]
        public async Task<IActionResult> GetByIdAsync(long typeId)
            => Ok(await _service.GetByIdAsync(typeId));

        [HttpGet("count")]
        public async Task<IActionResult> CountAsync()
            => Ok(await _service.CountAsync());

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] TypeCreateDto dto)
            => Ok(await _service.CreateAsync(dto));

        [HttpPut("{TypeId}")]
        public async Task<IActionResult> UpdateAsync(long typeId, [FromForm] TypeUpdateDto dto)
            => Ok(await _service.UpdateAsync(typeId, dto));

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long catalogId)
            => Ok(await _service.DeleteAsync(catalogId));

        //#region Products

        //[HttpGet("{catalogId}/products")]
        //public async Task<IActionResult> GetProductsByCatalodIdAsync(long catalogId, [FromQuery] PaginationParams @params)
        //    => Ok(catalogId);
        //#endregion
    }
}
