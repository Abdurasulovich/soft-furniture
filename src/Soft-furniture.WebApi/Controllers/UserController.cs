using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.Service.Dtos.Users;
using Soft_furniture.Service.Interfaces.Users;
using Soft_furniture.Service.Validators.Dtos.Users;

namespace Soft_furniture.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly int MaxPageSize = 30;

        public UserController(IUserService userService)
        {
            this._service = userService;
        }

        //Get All Users

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, MaxPageSize)));

        //Get by Id User

        [HttpGet("UserId")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(long UserId)
            => Ok(await _service.GetByIdAsync(UserId));

        //Count User

        [HttpGet("count")]
        [AllowAnonymous]
        public async Task<IActionResult> CountAsync()
            => Ok(await _service.CountAsync());

        //Search User
        [HttpGet("search/{search}")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchAsync(string search, [FromQuery] int page = 1)
        {
            var result = (await _service.SearchAsync(search, new PaginationParams(page, MaxPageSize)));
            return Ok(result.Item2);
        }

        //Delete User
        [HttpDelete("{UserId}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteAsync(long UserId)
            => Ok(await _service.DeleteAsync(UserId));

        //Update User
        [HttpPut]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateAsync(long UserId, [FromForm] UserUpdateDto userUpdate)
        {
            var validator = new UserUpdateValidator();
            var result = validator.Validate(userUpdate);
            if (result.IsValid)
            {
                return Ok(await _service.UpdateAsync(UserId, userUpdate));
            }
            else return BadRequest(result.Errors);
        }
    }
}
