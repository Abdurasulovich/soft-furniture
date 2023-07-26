using Soft_furniture.DataAccess.Utils;
using Soft_furniture.Domain.Entities.Products;
using Soft_furniture.Domain.Entities.Users;
using Soft_furniture.Service.Dtos.Products;
using Soft_furniture.Service.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soft_furniture.Service.Interfaces.Users;

public interface IUserService
{
    public Task<(bool Result, int CachedMinutes)> RegisterAsync(UserCreateDto dto);

    public Task<(bool Result, int CachedVerificationHours)> SendCodeForRegisterAsync(string phone);

    public Task<(bool Result, string Token)> VarifyRegisterAsync(string phone, int code);
}
