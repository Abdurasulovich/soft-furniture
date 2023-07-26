using Soft_furniture.Domain.Entities;

namespace Soft_furniture.DataAccess.ViewModels.Furniture_Types;

public class Furniture_typeViewModel : BaseEntity
{
    public string Name { get; set; } = String.Empty;

    public string ImagePath { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;
}
