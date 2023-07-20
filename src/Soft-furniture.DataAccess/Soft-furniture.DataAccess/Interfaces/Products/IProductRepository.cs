using Soft_furniture.DataAccess.Common.Interfaces;
using Soft_furniture.DataAccess.ViewModels.Products;
using Soft_furniture.Domain.Entities.Products;

namespace Soft_furniture.DataAccess.Interfaces.Products;

public interface IProductRepository : IRepository<Product, ProductViewModel>,
    IGetAll<ProductViewModel>, ISearchable<ProductViewModel>
{

}
