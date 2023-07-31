using Soft_furniture.DataAccess.Utils;
using Soft_furniture.DataAccess.ViewModels.Furniture_Types;
using Soft_furniture.Domain.Entities.Furniture_Type;
using Soft_furniture.Service.Dtos.Types;

namespace Soft_furniture.Service.Interfaces.Furniture_types;

public interface ITypeService
{
    public Task<bool> CreateAsync(TypeCreateDto dto);

    public Task<bool> DeleteAsync(long typeId);

    public Task<long> CountAsync();

    public Task<IList<Furniture_Type>> GetAllByCatalogIdAsync(long catalogId);

    public Task<Furniture_Type> GetByIdAsync(long typeId);

    public Task<bool> UpdateAsync(long typeId, TypeUpdateDto dto);
}
