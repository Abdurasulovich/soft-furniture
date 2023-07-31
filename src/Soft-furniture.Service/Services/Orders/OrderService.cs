using Soft_furniture.DataAccess.Interfaces.Orders;
using Soft_furniture.DataAccess.Interfaces.Products;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.DataAccess.ViewModels.Orders;
using Soft_furniture.Domain.Entities.Orders;
using Soft_furniture.Domain.Exceptions.Delivers;
using Soft_furniture.Domain.Exceptions.Orders;
using Soft_furniture.Service.Common.Helpers;
using Soft_furniture.Service.Dtos.Orders;
using Soft_furniture.Service.Interfaces.Auth;
using Soft_furniture.Service.Interfaces.Orders;

namespace Soft_furniture.Service.Services.Orders;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IProductRepository _productRepository;
    private readonly IIdentityService _identity;

    public OrderService(IOrderRepository repository,
        IProductRepository productRepository, IIdentityService identity)
    {
        this._repository = repository;
        this._productRepository = productRepository;
        this._identity=identity;
    }

    public async Task<long> CountAsync()
    {
        var result = await _repository.CountAsync();
        return result;
    }

    public async Task<bool> CreateAsync(OrderCreateDto dto)
    {
        var productId = _productRepository.GetByIdAsync(dto.ProductId).Result;
        if(productId == null) throw new OrderNotFoundException();
        bool paymentType = false;
        int delivered = 0;
        if(dto.PaymentType == Domain.Enums.PaymentType.ByCard)
            paymentType = true;
        if(dto.ShouldItDeliver == Domain.Enums.ShouldItDeliver.Yes)
            delivered = 100000;
        var nm = _identity.UserId;
        Order order = new Order()
        {
            UserId = _identity.UserId,
            DeliverId = 2,
            ProductId = dto.ProductId,
            ProductPrice = productId.UnitPrice,
            DeliveryPrice = delivered,
            TotalPrice = productId.UnitPrice + delivered,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            Status = Domain.Enums.OrderStatus.InQueue,
            IsContracted = true,
            Description = "Mahsulot 2 kun ichida yetkazilib beriladi!",
            PaymentType = dto.PaymentType,
            IsPaid = paymentType,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var result = await _repository.CreateAsync(order);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long orderId)
    {
        var result = await _repository.GetByIdAsync(orderId);
        if (result is null) throw new OrderNotFoundException(); 

        var dbResult = await _repository.DeleteAsync(orderId);
        return dbResult > 0;
    }

    public async Task<IList<Order>> GetAllAsync(PaginationParams @params)
    {
        var result = await _repository.GetAllAsync(@params);
        return result;
    }

    public async Task<IList<OrderVM>> GetAllUserOrderAsync()
    {
        var result = await _repository.GetAllUserOrderAsync(_identity.UserId);
        
        return result;
    }

    public async Task<Order?> GetByIdAsync(long orderId)
    {
        var result = await _repository.GetByIdAsync(orderId);
        if (result is null) { throw new OrderNotFoundException(); }
        else { return result; }
    }

    public async Task<bool> UpdateAsync(long orderId, OrderUpdateDto dto)
    {
        var order = await _repository.GetByIdAsync(orderId);
        if (order is null) { throw new DeliveryNotFoundException(); }

        order.ProductId = dto.ProductId;
        order.Latitude = dto.Latitude;
        order.Longitude = dto.Longitude;
        order.PaymentType = dto.PaymentType;

        var result = await _repository.UpdateAsync(orderId, order);
        return result > 0;
    }
}
