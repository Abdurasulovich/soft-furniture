using Soft_furniture.DataAccess.Common.Interfaces;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.Domain.Entities.Furniture_Catalog;

namespace Soft_furniture.DataAccess.Interfaces.Catalogs;

public class ICatalogRepository : IRepository<Catalog, Catalog>,
    IGetAll<Catalog>
{
    public Task<long> CountAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> CreateAsync(Catalog entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IList<Catalog>> GetAllAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<Catalog> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(long id, Catalog entity)
    {
        throw new NotImplementedException();
    }
}
