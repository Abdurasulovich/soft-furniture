using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.Service.Dtos.Delivers;
using Soft_furniture.Service.Interfaces.Delivers;
using Soft_furniture.Service.Validators.Dtos.Delivers;

namespace Soft_furniture.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliverController : ControllerBase
    {
        private readonly IDeliverService _service;
        private readonly int MaxPageSize = 30;

        public DeliverController(IDeliverService service)
        {
            this._service = service;
        }

        //Get All Delivers

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, MaxPageSize)));

        //Get by Id Deliver

        [HttpGet("DeliverId")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(long UserId)
            => Ok(await _service.GetByIdAsync(UserId));

        //Count Delivers

        [HttpGet("count")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CountAsync()
            => Ok(await _service.CountAsync());

        //Delete Deliver
        [HttpDelete("{DeliverId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long DeliverId)
            => Ok(await _service.DeleteAsync(DeliverId));

        //Update Deliver
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(long DeliverId, [FromForm] DeliverUpdateDto deliverUpdate)
        {
            var validator = new DeliverUpdateValidator();
            var result = validator.Validate(deliverUpdate);
            if (result.IsValid)
            {
                return Ok(await _service.UpdateAsync(DeliverId, deliverUpdate));
            }
            else return BadRequest(result.Errors);
        }
    }
}

