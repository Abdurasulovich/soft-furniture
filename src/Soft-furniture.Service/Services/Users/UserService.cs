using Microsoft.AspNetCore.Http;
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
using Soft_furniture.Service.Dtos.Security;
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

    public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string phone)
    {
        if(_memoryCache.TryGetValue(phone, out UserCreateDto userCreateDto))
        {
            VerificationDto verificationDto = new VerificationDto();
            verificationDto.Attempt = 0;
            verificationDto.CreatedAt = TimeHelper.GetDateTime();
            //make confirm code as random
            verificationDto.Code = 11111;
            _memoryCache.Set(phone, verificationDto,
                TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

            //sms sender::begin
            //sms sender::end


            return (Result: true, CachedVerificationHours: CACHED_MINUTES_FOR_VERIFICATION);
        }
        else throw new UserCacheDataExpiredException();
    }

    public async Task<(bool Result, string Token)> VarifyRegisterAsync(string phone, int code)
    {
        throw new NotImplementedException();
    }
}
