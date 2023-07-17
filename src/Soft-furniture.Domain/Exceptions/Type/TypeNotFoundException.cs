namespace Soft_furniture.Domain.Exceptions.Type;

public class TypeNotFoundException : NotFoundExeption
{
    public TypeNotFoundException()
    {
        this.TitleMessage = "Furniture type not found!";
    }
}
