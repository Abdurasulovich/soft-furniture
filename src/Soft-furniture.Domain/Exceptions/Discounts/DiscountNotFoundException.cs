namespace Soft_furniture.Domain.Exceptions.Discounts;

public class DiscountNotFoundException : NotFoundExeption
{
    public DiscountNotFoundException()
    {
        this.TitleMessage = "Discount not found!";
    }
}
