using Soft_furniture.DataAccess.Common.Interfaces;
using Soft_furniture.DataAccess.ViewModels.Users;
using Soft_furniture.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soft_furniture.DataAccess.Interfaces.Users;

public interface IUserRepository : IRepository<User, UserViewModel>,
    IGetAll<UserViewModel>, ISearchable<UserViewModel>
{
    public Task<User?> GetByPhoneAsync(string phone);
}
