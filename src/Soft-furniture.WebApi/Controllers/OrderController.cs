using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.Service.Dtos.Orders;
using Soft_furniture.Service.Dtos.Products;
using Soft_furniture.Service.Interfaces.Orders;
using Soft_furniture.Service.Validators.Dtos.Orders;
using Soft_furniture.Service.Validators.Dtos.Products;
using System.Runtime.CompilerServices;

namespace Soft_furniture.WebApi.Controllers
{
    [Route("api/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderService _orderService;
        private readonly int maxPageSize = 30;
        public OrderController(IOrderService service)
        {
            this._orderService = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]


        public async Task<IActionResult> GetAllAsync(int page = 1)
        => Ok(await _orderService.GetAllAsync(new PaginationParams(page, maxPageSize)));

        [HttpGet("orderId")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetByIdAsync(long orderId)
        => Ok(await _orderService.GetByIdAsync(orderId));

        [HttpGet("GetAllMyOrders")]
        [Authorize(Roles = "Admin, User")]

        public async Task<IActionResult> GetAllUserOrderAsync()
            =>Ok(await _orderService.GetAllUserOrderAsync());

        [HttpGet("count")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> CountAsync()
            => Ok(await _orderService.CountAsync());

        [HttpPost]
        [Authorize(Roles ="Admin, User")]
        public async Task<IActionResult> CreateAsync([FromForm] OrderCreateDto dto)
        {
            var createValidator = new OrderCreateValidator();
            var result = createValidator.Validate(dto);

            if (result.IsValid) return Ok(await _orderService.CreateAsync(dto));
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPut("productId")]
        [Authorize(Roles = "Admin, User")]

        public async Task<IActionResult> UpdateAsync(long orderId, [FromForm] OrderUpdateDto dto)
        {
            var updateValidator = new OrderUpdateValidator();
            var validationResult = updateValidator.Validate(dto);
            if (validationResult.IsValid) return Ok(await _orderService.UpdateAsync(orderId, dto));
            else return BadRequest(validationResult.Errors);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> DeleteAsync(long productId)
            => Ok(await _orderService.DeleteAsync(productId));
    }
}
