﻿using Soft_furniture.DataAccess.Common.Interfaces;
using Soft_furniture.DataAccess.ViewModels.Orders;
using Soft_furniture.Domain.Entities.Orders;

namespace Soft_furniture.DataAccess.Interfaces.Orders;

public interface IOrderRepository : IRepository<Order, Order>, IGetAll<Order>
{
    public Task<IList<OrderVM>> GetAllUserOrderAsync(long userId);
}
