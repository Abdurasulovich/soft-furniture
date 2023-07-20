using Microsoft.AspNetCore.Http;

namespace Soft_furniture.Service.Dtos.Media;

public class ImageCreateDto
{
    public IFormFile File { get; set; } = default!;
}
