using Microsoft.Extensions.Caching.Memory;
using Soft_furniture.DataAccess.Interfaces.Products;
using Soft_furniture.DataAccess.Interfaces.Users;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.Domain.Entities.Products;
using Soft_furniture.Domain.Entities.Users;
using Soft_furniture.Domain.Exceptions.Catalog;
using Soft_furniture.Domain.Exceptions.Files;
using Soft_furniture.Domain.Exceptions.Product;
using Soft_furniture.Domain.Exceptions.Users;
using Soft_furniture.Service.Common.Helpers;
using Soft_furniture.Service.Dtos.Products;
using Soft_furniture.Service.Dtos.Users;
using Soft_furniture.Service.Interfaces.Common;
using Soft_furniture.Service.Interfaces.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soft_furniture.Service.Services.Users;

public class UserService : IUserService
{
    private IMemoryCache _memoryCache;
    private IUserRepository _userRepository;
    private const int CACHED_MINUTES_FOR_REGISTER = 60;
    private const int CACHED_MINUTES_FOR_VERIFICATION = 5;

    public UserService(IMemoryCache memoryCache, IUserRepository userRepository)
    {
        this._memoryCache = memoryCache;
        this._userRepository = userRepository;
    }
    public async Task<(bool Result, int CachedMinutes)> RegisterAsync(UserCreateDto dto)
    {
        var user = await _userRepository.GetByPhoneAsync(dto.PhoneNumber);
        if (user is not null) throw new UserAlreadyExistsException(dto.PhoneNumber);

        // delete if exists user by this phone number
        if(_memoryCache.TryGetValue(dto.PhoneNumber, out UserCreateDto cachedUserCreateDto))
         {
            cachedUserCreateDto.FirstName = cachedUserCreateDto.FirstName;
            _memoryCache.Remove(dto.PhoneNumber);
        }
        else _memoryCache.Set(dto.PhoneNumber, dto, 
            TimeSpan.FromMinutes(CACHED_MINUTES_FOR_REGISTER));

        return (Result: true, CachedMinutes: CACHED_MINUTES_FOR_REGISTER);
    }

    public Task<(bool Result, int CachedVerificationHours)> SendCodeForRegisterAsync(string phone)
    {
        throw new NotImplementedException();
    }

    public Task<(bool Result, string Token)> VarifyRegisterAsync(string phone, int code)
    {
        throw new NotImplementedException();
    }
}
