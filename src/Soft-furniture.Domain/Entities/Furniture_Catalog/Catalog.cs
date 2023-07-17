using System.ComponentModel.DataAnnotations;

namespace Soft_furniture.Domain.Entities.Furniture_Catalog;

public class Catalog : Auditable
{
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public string ImagePath { get; set; } = string.Empty;
}
