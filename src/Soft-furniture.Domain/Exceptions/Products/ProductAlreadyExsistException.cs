namespace Soft_furniture.Domain.Exceptions.Product;

public class ProductAlreadyExsistException : AlreadyExsistException
{
    public ProductAlreadyExsistException()
    {
        this.TitleMessage = "You have already added this product!";
    }
}
