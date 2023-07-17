namespace Soft_furniture.Domain.Exceptions.Users;

public class UserNotFoundException : NotFoundExeption
{
    public UserNotFoundException()
    {
        this.TitleMessage = "User not found!";
    }
}
