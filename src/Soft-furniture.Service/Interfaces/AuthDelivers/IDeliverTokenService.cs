using Soft_furniture.Domain.Entities.Delivers;

namespace Soft_furniture.Service.Interfaces.Delivers;

public interface IDeliverTokenService
{
    public string GenerateToken(Deliver deliver);
}
