namespace Soft_furniture.Domain.Entities.Furniture_Type;

public class Type : Auditable
{
    public string Name { get; set; } = String.Empty;
    public long FurnitureCatalogId { get; set; }
    public string Description { get; set; } = String.Empty;
}
