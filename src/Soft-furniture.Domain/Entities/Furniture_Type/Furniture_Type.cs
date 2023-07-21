namespace Soft_furniture.Domain.Entities.Furniture_Type;
public class Furniture_Type : Auditable
{
    public string Name { get; set; } = String.Empty;
    
    public long FurnitureCatalogId { get; set; }

    public string ImagePath { get; set; } = String.Empty;
    
    public string Description { get; set; } = String.Empty;
}
