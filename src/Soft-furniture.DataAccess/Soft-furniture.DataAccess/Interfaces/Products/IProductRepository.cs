using Soft_furniture.DataAccess.Common.Interfaces;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.Domain.Entities.Products;

namespace Soft_furniture.DataAccess.Interfaces.Products;

public interface IProductRepository : IRepository<Product, Product>,
    ISearchable<Product>
{
    public Task<IList<Product>> GetAllByTypeIdAsync(long typeId, PaginationParams @params);
    public Task<Product> GetAllByProductNameAsync(string productName);
}
