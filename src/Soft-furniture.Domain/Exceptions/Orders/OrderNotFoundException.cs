namespace Soft_furniture.Domain.Exceptions.Orders;

public class OrderNotFoundException : NotFoundExeption
{
    public OrderNotFoundException()
    {
        this.TitleMessage = "Order not found!";
    }
}
