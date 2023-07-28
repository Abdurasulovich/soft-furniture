
using Soft_furniture.Domain.Entities.Users;

public interface ITokenService
{
    public string GenerateToken(User user);
}
