using Soft_furniture.DataAccess.Utils;
using Soft_furniture.DataAccess.ViewModels.Users;
using Soft_furniture.Service.Dtos.Users;

namespace Soft_furniture.Service.Interfaces.Users;

public interface IUserService
{
    public Task<bool> DeleteAsync(long UserId);

    public Task<long> CountAsync();

    public Task<IList<UserViewModel>> GetAllAsync(PaginationParams @params);

    public Task<UserViewModel?> GetByIdAsync(long UserId);

    public Task<bool> UpdateAsync(long UserId, UserUpdateDto userUpdateDto);

    public Task<(long ItemsCount, IList<UserViewModel>)> SearchAsync(string search, PaginationParams @params);
}
