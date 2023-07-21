using Microsoft.AspNetCore.Mvc;
using Soft_furniture.Domain.Entities.Products;

namespace Soft_furniture.WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //[HttpGet]
        //public async Task<IActionResult> GetAsync([FromQuery] long id)
        //{ 
        //    if(id > 0) return Forbid("Access Denied");
        //}
    }
}
