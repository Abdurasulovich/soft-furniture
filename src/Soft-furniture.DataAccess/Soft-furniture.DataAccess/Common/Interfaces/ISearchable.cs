using Soft_furniture.DataAccess.Utils;

namespace Soft_furniture.DataAccess.Common.Interfaces;

public interface ISearchable<TModel>
{
    public Task<(int ItemsCount, IList<TModel>)> SearchAsync(string search, PaginationParams @params);
}
