using Soft_furniture.DataAccess.Common.Interfaces;
using Soft_furniture.Domain.Entities.Furniture_Catalog;

namespace Soft_furniture.DataAccess.Interfaces.Catalogs;

public interface ICatalogRepository : IRepository<Catalog, Catalog>,
    IGetAll<Catalog>
{

}
