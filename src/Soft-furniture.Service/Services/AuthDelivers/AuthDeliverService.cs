using Microsoft.Extensions.Caching.Memory;
using Soft_furniture.DataAccess.Interfaces.Delivers;
using Soft_furniture.Domain.Entities.Delivers;
using Soft_furniture.Domain.Exceptions;
using Soft_furniture.Domain.Exceptions.Auth;
using Soft_furniture.Domain.Exceptions.Delivers;
using Soft_furniture.Domain.Exceptions.Users;
using Soft_furniture.Service.Common.Helpers;
using Soft_furniture.Service.Common.Security;
using Soft_furniture.Service.Dtos.Auth;
using Soft_furniture.Service.Dtos.Delivers;
using Soft_furniture.Service.Dtos.Notifications;
using Soft_furniture.Service.Dtos.Security;
using Soft_furniture.Service.Interfaces.AuthDelivers;
using Soft_furniture.Service.Interfaces.Delivers;
using Soft_furniture.Service.Interfaces.Notifications;

namespace Soft_furniture.Service.Services.AuthDelivers;

public class AuthDeliverService : IAuthDeliverService

{
    private readonly IMemoryCache _memoryCache;
    private readonly IDeliveryRepository _deliverRepository;
    private readonly ISmsSender _smsSender;
    private readonly IDeliverTokenService _tokenService;
    private const int CACHED_MINUTES_FOR_REGISTER = 60;
    private const int CACHED_MINUTES_FOR_VERIFICATION = 5;
    private const string REGISTER_CACHE_KEY = "register_";
    private const string VERIFY_REGISTER_CACHE_KEY = "verify_register_";
    private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;
    public AuthDeliverService(IMemoryCache memoryCache,
        IDeliveryRepository deliverRepository,
        ISmsSender smsSender,
        IDeliverTokenService tokenService)
    {
        this._memoryCache = memoryCache;
        this._deliverRepository = deliverRepository;
        this._smsSender = smsSender;
        this._tokenService = tokenService;
    }

#pragma warning disable
    public async Task<(bool Result, int CachedMinutes)> RegisterAsync(DeliverCreateDto dto)
    {
        var user = await _deliverRepository.GetByPhoneAsync(dto.PhoneNumber);
        if (user is not null) throw new DeliverAlreadyExistsException(dto.PhoneNumber);

        // delete if exists user by this phone number
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + dto.PhoneNumber, out DeliverCreateDto cachedRegisterDto))
        {
            cachedRegisterDto.FirstName = cachedRegisterDto.FirstName;
            _memoryCache.Remove(REGISTER_CACHE_KEY + dto.PhoneNumber);
        }
        else _memoryCache.Set(REGISTER_CACHE_KEY + dto.PhoneNumber, dto,
            TimeSpan.FromMinutes(CACHED_MINUTES_FOR_REGISTER));

        return (Result: true, CachedMinutes: CACHED_MINUTES_FOR_REGISTER);
    }

    public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string phone)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out DeliverCreateDto registerDto))
        {
            VerificationDto verificationDto = new VerificationDto();
            verificationDto.Attempt = 0;
            verificationDto.CreatedAt = TimeHelper.GetDateTime();

            // make confirm code as random
            verificationDto.Code = CodeGenerator.GenerateRandomNumber();

            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + phone, out VerificationDto oldVerifcationDto))
            {
                _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + phone);
            }

            _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phone, verificationDto,
                TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

            SmsMessage smsMessage = new SmsMessage();
            smsMessage.Title = "SoftFurniture";
            smsMessage.Content = "Your verification code : " + verificationDto.Code;
            smsMessage.Recipent = phone.Substring(1);

            var smsResult = await _smsSender.SendAsync(smsMessage);
            if (smsResult is true) return (Result: true, CachedVerificationMinutes: CACHED_MINUTES_FOR_VERIFICATION);
            else return (Result: false, CachedVerificationMinutes: 0);
        }
        else throw new DeliverCacheDataExpiredException();
    }

    public async Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, int code)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out DeliverCreateDto registerDto))
        {
            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + phone, out VerificationDto verificationDto))
            {
                if (verificationDto.Attempt >= VERIFICATION_MAXIMUM_ATTEMPTS)
                    throw new VerificationTooManyRequestException();

                else if (verificationDto.Code == code)
                {
                    var dbResult = await RegisterToDatabaseAsync(registerDto);
                    if (dbResult is true)
                    {
                        var deliver = await _deliverRepository.GetByPhoneAsync(phone);
                        if (deliver == null) return (Result: false, Token: "");
                        string token = _tokenService.GenerateToken(deliver);
                        return (Result: true, Token: token);
                    }
                    else return (Result: false, Token: "");
                }
                else
                {
                    _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + phone);
                    verificationDto.Attempt++;
                    _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phone, verificationDto,
                        TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));
                    return (Result: false, Token: "");
                }
            }
            else throw new VerificationCodeExpiredException();
        }
        else throw new DeliverCacheDataExpiredException();
    }

    private async Task<bool> RegisterToDatabaseAsync(DeliverCreateDto registerDto)
    {
        var user = new Deliver();
        user.FirstName = registerDto.FirstName;
        user.LastName = registerDto.LastName;
        user.PhoneNumber = registerDto.PhoneNumber;
        user.PhoneNumberConfirmed = true;
        user.IsMale = registerDto.IsMale;
        user.BirthDate = registerDto.BirthDate;
        user.PasspordSeriaNumber = registerDto.PasspordSeriaNumber;
        user.Country = registerDto.Country;
        user.Region = registerDto.Region;
        user.City = registerDto.City;
        user.Address = registerDto.Address;
        user.Description = registerDto.Description;

        var hasherResult = PasswordHasher.Hash(registerDto.PasswordHash);
        user.PasswordHash = hasherResult.Hash;
        user.Salt = hasherResult.Salt;

        user.CreatedAt = user.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _deliverRepository.CreateAsync(user);
        return dbResult > 0;
    }

    public async Task<(bool Result, string Token)> LoginAsync(DeliverCreateDto loginDto)
    {
        var user = await _deliverRepository.GetByPhoneAsync(loginDto.PhoneNumber);
        if (user is null) throw new UserNotFoundException();

        var hasherResult = PasswordHasher.Verify(loginDto.PasswordHash, user.PasswordHash, user.Salt);
        if (hasherResult == false) throw new PasswordNotMatchException();

        string token = _tokenService.GenerateToken(user);
        return (Result: true, Token: token);
    }

    public async Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto)
    {
        var user = await _deliverRepository.GetByPhoneAsync(loginDto.PhoneNumber);
        if (user is null) throw new DeliveryNotFoundException();

        var hasherResult = PasswordHasher.Verify(loginDto.Password, user.PasswordHash, user.Salt);
        if (hasherResult == false) throw new PasswordNotMatchException();

        string token = _tokenService.GenerateToken(user);
        return (Result: true, Token: token);
    }
}
