using Soft_furniture.DataAccess.Utils;
using Soft_furniture.DataAccess.ViewModels.Furniture_Types;
using Soft_furniture.DataAccess.ViewModels.Orders;
using Soft_furniture.Domain.Entities.Furniture_Type;
using Soft_furniture.Domain.Entities.Orders;
using Soft_furniture.Service.Dtos.Orders;
using Soft_furniture.Service.Dtos.Types;

namespace Soft_furniture.Service.Interfaces.Orders;

public interface IOrderService
{
    public Task<bool> CreateAsync(OrderCreateDto dto);

    public Task<bool> DeleteAsync(long orderId);
    
    public Task<long> CountAsync();

    public Task<IList<Order>> GetAllAsync(PaginationParams @params);

    public Task<IList<OrderVM>> GetAllUserOrderAsync();

    public Task<Order?> GetByIdAsync(long orderId);

    public Task<bool> UpdateAsync(long orderId, OrderUpdateDto dto);



}
