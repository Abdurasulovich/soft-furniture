using Soft_furniture.Domain.Entities.Users;

namespace EnterComputers.Service.Interfaces.Auth
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
