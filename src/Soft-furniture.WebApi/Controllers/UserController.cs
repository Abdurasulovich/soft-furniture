using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Soft_furniture.Service.Dtos.Users;
using Soft_furniture.Service.Interfaces.Users;
using Soft_furniture.Service.Validators;
using Soft_furniture.Service.Validators.Dtos.Users;

namespace Soft_furniture.WebApi.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        this._userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromForm] UserCreateDto userCreateDto)
    {
        var validator = new UserCreateValidator();
        var result = validator.Validate(userCreateDto);
        if (result.IsValid)
        {
            var serviceResult = await _userService.RegisterAsync(userCreateDto);
            return Ok(new {serviceResult.Result, serviceResult.CachedMinutes});
        }
        else return BadRequest(result.Errors);
    }

    [HttpPost("register/send-code")]
    public async Task<IActionResult> SendCodeRegisterAsync(string phone)
    {
        var result = PhoneNumberValidator.IsValid(phone);
        if (result == false) return BadRequest("Phone number is invalid!");
        
        var serviceResult = await _userService.SendCodeForRegisterAsync(phone);
        return Ok(new {serviceResult.Result, serviceResult.CachedVerificationMinutes  });
    }
}
