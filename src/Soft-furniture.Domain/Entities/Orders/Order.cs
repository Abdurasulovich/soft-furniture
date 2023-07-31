using Soft_furniture.Domain.Enums;

namespace Soft_furniture.Domain.Entities.Orders;

public class Order : Auditable
{
    public long UserId { get; set; }

    public long DeliverId { get; set; }

    public long ProductId { get; set; }

    public double ProductPrice { get; set; }

    public double DeliveryPrice { get; set; }

    public double TotalPrice { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public OrderStatus Status { get; set; }

    public bool IsContracted { get; set; }

    public string Description { get; set; } = string.Empty;

    public bool IsPaid { get; set; }

    public PaymentType PaymentType { get; set; }
}
