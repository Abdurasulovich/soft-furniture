using Soft_furniture.DataAccess.Common.Interfaces;
using Soft_furniture.Domain.Entities.Discounts;

namespace Soft_furniture.DataAccess.Interfaces.Discounts;

public interface IDiscountRepository : IRepository<Discount, Discount>,
    IGetAll<Discount>
{
}
