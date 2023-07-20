using Microsoft.AspNetCore.Http;

namespace Soft_furniture.Service.Dtos.Media;

public class AvatarCreateDto
{
    public IFormFile Avatar { get; set; } = default!;
}
