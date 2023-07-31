namespace Soft_furniture.Domain.Exceptions.Type;

public class AlreadyExistException : NotFoundExeption
{
    public AlreadyExistException()
    {
        this.TitleMessage = "You have already added this type!";
    }
}
