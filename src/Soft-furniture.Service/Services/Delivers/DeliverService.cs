using Soft_furniture.DataAccess.Interfaces.Delivers;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.DataAccess.ViewModels.Delivers;
using Soft_furniture.Domain.Exceptions.Delivers;
using Soft_furniture.Domain.Exceptions.Users;
using Soft_furniture.Service.Common.Helpers;
using Soft_furniture.Service.Common.Security;
using Soft_furniture.Service.Dtos.Delivers;
using Soft_furniture.Service.Interfaces.Delivers;

namespace Soft_furniture.Service.Services.Delivers;

public class DeliverService : IDeliverService
{
    private readonly IDeliveryRepository _repository;

    public DeliverService(IDeliveryRepository repository)
    {
        this._repository = repository;
    }
    public async Task<long> CountAsync()
    {
        var result = await _repository.CountAsync();
        return result;
    }

    public async Task<bool> DeleteAsync(long DeliverId)
    {
        var result = await _repository.GetByIdAsync(DeliverId);
        if (result is null) { throw new UserNotFoundException(); }

        var dbResult = await _repository.DeleteAsync(DeliverId);
        return dbResult > 0;
    }

    public async Task<IList<DeliverViewModel>> GetAllAsync(PaginationParams @params)
    {
        var result = await _repository.GetAllAsync(@params);
        return result;
    }

    public async Task<DeliverViewModel?> GetByIdAsync(long DeliverId)
    {
        var result = await _repository.GetByIdAsync(DeliverId);
        if (result is null) { throw new DeliveryNotFoundException(); }
        else { return result; }
    }

    public async Task<bool> UpdateAsync(long UserId, DeliverUpdateDto userUpdateDto)
    {
        var user = await _repository.GetByIdCheckUser(UserId);
        if (user is null) { throw new DeliveryNotFoundException(); }

        user.FirstName = userUpdateDto.FirstName;
        user.LastName = userUpdateDto.LastName;
        user.PhoneNumber = userUpdateDto.PhoneNumber;
        user.PhoneNumberConfirmed = true;
        user.IsMale = userUpdateDto.IsMale;

        var hash = PasswordHasher.Hash(userUpdateDto.PasswordHash);
        user.PasswordHash = hash.Hash;
        user.Salt = hash.Salt;

        user.BirthDate = userUpdateDto.BirthDate;
        user.PasspordSeriaNumber = userUpdateDto.PasspordSeriaNumber;
        user.Country = userUpdateDto.Country;
        user.Region = userUpdateDto.Region;
        user.City = userUpdateDto.City;
        user.Address = userUpdateDto.Address;
        user.Description = userUpdateDto.Description;
        user.UpdatedAt = TimeHelper.GetDateTime();

        var result = await _repository.UpdateAsync(UserId, user);
        return result > 0;
    }
}
