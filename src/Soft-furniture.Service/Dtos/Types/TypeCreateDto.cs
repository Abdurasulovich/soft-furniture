using Microsoft.AspNetCore.Http;
using Soft_furniture.Domain.Entities;

namespace Soft_furniture.Service.Dtos.Types;

public class TypeCreateDto
{
    public string Name { get; set; } = String.Empty;

    public long FurnitureCatalogId { get; set; }

    public string Description { get; set; } = String.Empty;

    public IFormFile ImagePath { get; set; } = default!;
}
