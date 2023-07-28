using Soft_furniture.DataAccess.Utils;
using Soft_furniture.Domain.Entities.Furniture_Catalog;
using Soft_furniture.Service.Dtos.Catalogs;

namespace Soft_furniture.Service.Interfaces.Catalogs;

public interface ICatalogService
{
    public Task<bool> CreateAsync(CatalogCreateDto dto);

    public Task<bool> DeleteAsync(long catalogId);

    public Task<long> CountAsync();

    public Task<IList<Catalog>> GetAllAsync(PaginationParams @params);

    public Task<Catalog> GetByIdAsync(long catalogId);

    public Task<bool> UpdateAsync(long catalogId, CatalogUpdateDto dto);
}
