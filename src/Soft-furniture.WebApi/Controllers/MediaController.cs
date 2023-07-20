using Microsoft.AspNetCore.Mvc;
using Soft_furniture.Service.Dtos.Media;

namespace Soft_furniture.WebApi.Controllers
{
    [Route("api/media")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        [HttpPost("images")]
        public async Task<string> CreateImageAsyc([FromForm] ImageCreateDto imageDto)
        {
            return "";
        }

        [HttpPost("avatar")]
        public async Task<string> CreateAvatarAsnyc([FromForm] AvatarCreateDto avatarDto)
        {
            return "";
        }
    }
}
