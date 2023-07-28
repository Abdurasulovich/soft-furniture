using Soft_furniture.Service.Dtos.Auth;
using Soft_furniture.Service.Dtos.Delivers;

namespace Soft_furniture.Service.Interfaces.AuthDelivers;

public interface IAuthDeliverService
{
    public Task<(bool Result, int CachedMinutes)> RegisterAsync(DeliverCreateDto dto);

    public Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string phone);

    public Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, int code);

    public Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto);
}
