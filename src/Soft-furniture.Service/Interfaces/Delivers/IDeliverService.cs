


using Soft_furniture.DataAccess.Utils;
using Soft_furniture.DataAccess.ViewModels.Delivers;
using Soft_furniture.Service.Dtos.Delivers;

namespace Soft_furniture.Service.Interfaces.Delivers;

public interface IDeliverService
{
    public Task<bool> DeleteAsync(long DeliverId);

    public Task<long> CountAsync();

    public Task<IList<DeliverViewModel>> GetAllAsync(PaginationParams @params);

    public Task<DeliverViewModel?> GetByIdAsync(long DeliverId);

    public Task<bool> UpdateAsync(long UserId, DeliverUpdateDto userUpdateDto);

}
