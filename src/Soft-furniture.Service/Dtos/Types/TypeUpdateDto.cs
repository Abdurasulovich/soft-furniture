using Microsoft.AspNetCore.Http;

namespace Soft_furniture.Service.Dtos.Types;

public class TypeUpdateDto
{
    public string Name { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

    public IFormFile? ImagePath { get; set; }
}
