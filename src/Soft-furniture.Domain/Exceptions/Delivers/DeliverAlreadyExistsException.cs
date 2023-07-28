namespace Soft_furniture.Domain.Exceptions.Delivers;

public class DeliverAlreadyExistsException : AlreadyExsistException
{
    public DeliverAlreadyExistsException()
    {
        TitleMessage = "Deliver already exists!";
    }

    public DeliverAlreadyExistsException(string phone)
    {
        TitleMessage = "This phone already registered!";
    }
}
