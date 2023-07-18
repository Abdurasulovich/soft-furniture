using Soft_furniture.DataAccess.Utils;

namespace Soft_furniture.DataAccess.Common.Interfaces;

public interface IGetAll<TModel>
{
    public Task<IList<TModel>> GetAllAsync(PaginationParams @params);
}