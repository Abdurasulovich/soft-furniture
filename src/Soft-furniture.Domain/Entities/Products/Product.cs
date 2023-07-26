using System.ComponentModel.DataAnnotations;

namespace Soft_furniture.Domain.Entities.Products;

public class Product : Auditable
{
    [MaxLength(50)]
    public string Name { get; set; } = String.Empty;

    public string ImagePath { get; set; } = String.Empty;

    public long FurnitureTypeId { get; set; }

    public double UnitPrice { get; set; }

    public string Description { get; set; } = String.Empty;
}
