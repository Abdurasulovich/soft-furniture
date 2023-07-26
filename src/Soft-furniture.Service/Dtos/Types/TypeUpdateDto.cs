using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Soft_furniture.Service.Dtos.Types;

public class TypeUpdateDto
{
    [MaxLength(50)]
    public string Name { get; set; } = String.Empty;

    public long FurnitureCatalogId { get; set; }

    public string Description { get; set; } = String.Empty;

    public IFormFile? ImagePath { get; set; }
}
