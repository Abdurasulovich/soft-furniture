using Soft_furniture.Service.Dtos.Catalogs;

namespace Soft_furniture.Service.Interfaces.Catalogs;

public interface ICatalogService
{
    public Task<bool> CreateAsync(CatalogCreateDto dto);

    public Task<bool> DeleteAsync(long catalogId);

    public Task<long> CountAsync();
}
