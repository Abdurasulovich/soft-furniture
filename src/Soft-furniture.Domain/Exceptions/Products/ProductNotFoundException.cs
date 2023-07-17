namespace Soft_furniture.Domain.Exceptions.Product;

public class ProductNotFoundException : NotFoundExeption
{
    public ProductNotFoundException()
    {
        this.TitleMessage = "Product not found!";
    }
}
