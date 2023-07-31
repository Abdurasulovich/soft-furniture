using Soft_furniture.Domain.Enums;

namespace Soft_furniture.Service.Dtos.Orders;

public class OrderCreateDto
{
    public long ProductId { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public ShouldItDeliver ShouldItDeliver { get; set; }

    public PaymentType PaymentType { get; set; }

}
