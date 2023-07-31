using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soft_furniture.Service.Dtos.Auth;
using Soft_furniture.Service.Dtos.Delivers;
using Soft_furniture.Service.Interfaces.AuthDelivers;
using Soft_furniture.Service.Validators;
using Soft_furniture.Service.Validators.Dtos.Auth;
using Soft_furniture.Service.Validators.Dtos.Delivers;

namespace Soft_furniture.WebApi.Controllers
{
    [Route("api/DeliverRegister")]
    [ApiController]
    public class DeliverRegisterController : ControllerBase
    {
        private IAuthDeliverService _deliverService;

        public DeliverRegisterController(IAuthDeliverService deliverService)
        {
            this._deliverService = deliverService;
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> RegisterAsync([FromForm] DeliverCreateDto authCreateDto)
        {
            var validator = new DeliverCreateValidator();
            var result = validator.Validate(authCreateDto);
            if (result.IsValid)
            {
                var serviceResult = await _deliverService.RegisterAsync(authCreateDto);
                return Ok(new { serviceResult.Result, serviceResult.CachedMinutes });
            }
            else return BadRequest(result.Errors);
        }

        [HttpPost("register/send-code")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> SendCodeRegisterAsync(string phone)
        {
            var result = PhoneNumberValidator.IsValid(phone);
            if (result == false) return BadRequest("Phone number is invalid!");

            var serviceResult = await _deliverService.SendCodeForRegisterAsync(phone);
            return Ok(new { serviceResult.Result, serviceResult.CachedVerificationMinutes });
        }

        [HttpPost("register/verify")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> VerifyRegisterAsync([FromBody] VerifyRegisterDto verifyRegisterDto)
        {
            var serviceResult = await _deliverService.VerifyRegisterAsync(verifyRegisterDto.PhoneNumber, verifyRegisterDto.Code);
            return Ok(new { serviceResult.Result, serviceResult.Token });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromQuery] LoginDto loginDto)
        {
            var validator = new LoginValidator();
            var valResult = validator.Validate(loginDto);
            if (valResult.IsValid == false) return BadRequest(valResult.Errors);

            var serviceResult = await _deliverService.LoginAsync(loginDto);
            return Ok(new { serviceResult.Result, serviceResult.Token });
        }
    }
}
