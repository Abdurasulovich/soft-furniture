using Microsoft.AspNetCore.Http;

namespace Soft_furniture.Service.Dtos.Catalogs;

public class CatalogCreateDto
{
    public string Name { get; set; } = String.Empty;

    public IFormFile ImagePath { get; set; } = default!;
}
