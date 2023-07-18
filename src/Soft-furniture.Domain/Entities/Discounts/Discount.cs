using System.ComponentModel.DataAnnotations;

namespace Soft_furniture.Domain.Entities.Discounts;

public class Discount : Auditable
{
    [MaxLength(50)]
    public string Name { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

}
