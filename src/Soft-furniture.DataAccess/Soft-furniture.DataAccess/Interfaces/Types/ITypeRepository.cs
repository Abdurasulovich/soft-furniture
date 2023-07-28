using Soft_furniture.DataAccess.Common.Interfaces;
using Soft_furniture.DataAccess.ViewModels.Furniture_Types;
using Soft_furniture.Domain.Entities.Furniture_Type;

namespace Soft_furniture.DataAccess.Interfaces.Types;

public interface ITypeRepository : IRepository<Furniture_Type, Furniture_Type>,
    IGetAll<Furniture_typeViewModel>
{

}
