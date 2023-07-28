namespace Soft_furniture.Domain.Exceptions.Users;

public class UserAlreadyExistsException : AlreadyExsistException
{
    public UserAlreadyExistsException()
    {
        TitleMessage = "User already exists!";
    }

    public UserAlreadyExistsException(string phone)
    {
        TitleMessage = "This phone already registered";
    }
}
