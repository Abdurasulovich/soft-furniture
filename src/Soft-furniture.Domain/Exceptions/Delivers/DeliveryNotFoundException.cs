namespace Soft_furniture.Domain.Exceptions.Delivers;

public class DeliveryNotFoundException : NotFoundExeption
{
    public DeliveryNotFoundException()
    {
        this.TitleMessage = "Deliver not found!";
    }
}
