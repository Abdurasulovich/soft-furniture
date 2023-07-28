using Soft_furniture.DataAccess.Interfaces.Users;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.DataAccess.ViewModels.Users;
using Soft_furniture.Domain.Exceptions.Users;
using Soft_furniture.Service.Common.Helpers;
using Soft_furniture.Service.Dtos.Users;
using Soft_furniture.Service.Interfaces.Users;

namespace Soft_furniture.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    public UserService(IUserRepository userRepository)
    {
        this._repository = userRepository;
    }

    public async Task<long> CountAsync()
    {
        var result = await _repository.CountAsync();
        return result;
    }

    public async Task<bool> DeleteAsync(long UserId)
    {
        var result = await _repository.GetByIdAsync(UserId);
        if (result is null) { throw new UserNotFoundException(); }

        var dbResult = await _repository.DeleteAsync(UserId);
        return dbResult > 0;
    }

    public async Task<IList<UserViewModel>> GetAllAsync(PaginationParams @params)
    {
        var result = await _repository.GetAllAsync(@params);
        return result;
    }

    public async Task<UserViewModel?> GetByIdAsync(long UserId)
    {
        var result = await _repository.GetByIdAsync(UserId);
        if (result is null) { throw new UserNotFoundException(); }
        else { return result; }
    }

    public async Task<(long ItemsCount, IList<UserViewModel>)> SearchAsync(string search, PaginationParams @params)
    {
        var result = await _repository.SearchAsync(search, @params);
        return result;
    }

    public async Task<bool> UpdateAsync(long UserId, UserUpdateDto userUpdateDto)
    {
        var user = await _repository.GetByIdCheckUser(UserId);
        if (user is null) { throw new UserNotFoundException(); }

        user.FirstName = userUpdateDto.FirstName;
        user.LastName = userUpdateDto.LastName;
        user.PhoneNumber = userUpdateDto.PhoneNumber;
        user.PhoneNumberConfirmed = true;
        user.Country = userUpdateDto.Country;
        user.Region = userUpdateDto.Region;
        user.City = userUpdateDto.City;
        user.Address = userUpdateDto.Address;
        user.IdentityRole = Domain.Enums.IdentityRole.User;
        user.UpdatedAt = TimeHelper.GetDateTime();

        var result = await _repository.UpdateAsync(UserId, user);
        return result > 0;
    }
}
