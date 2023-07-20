namespace Soft_furniture.Domain.Entities.Orders;

public class OrderDetail : Auditable
{
    public long OrderId { get; set; }

    public long ProductId { get; set; }

    public int Quantiy { get; set; }

    //Price price of the products
    //Product price * quantity
    public double TotalPrice { get; set; }

    public double DiscountPrice { get; set; }

    //Price that user must pay for these products
    //TotalPrice - DiscountPrice
    public double ResultPrice { get; set; }
}
