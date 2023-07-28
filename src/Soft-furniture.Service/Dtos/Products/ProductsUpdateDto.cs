using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Soft_furniture.Service.Dtos.Products;

public class ProductsUpdateDto
{
    [MaxLength(50)]
    public string Name { get; set; } = String.Empty;

    public IFormFile? ImagePath { get; set; }

    public long FurnitureTypeId { get; set; }

    public double UnitPrice { get; set; }

    public string Description { get; set; } = String.Empty;
}
