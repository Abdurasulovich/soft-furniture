using Soft_furniture.DataAccess.Common.Interfaces;
using Soft_furniture.DataAccess.ViewModels.Delivers;
using Soft_furniture.Domain.Entities.Delivers;

namespace Soft_furniture.DataAccess.Interfaces.Delivers;

public interface IDeliveryRepository : IRepository<Deliver, DeliverViewModel>,
    IGetAll<DeliverViewModel>
{
    public Task<Deliver?> GetByPhoneAsync(string phone);

    public Task<Deliver?> GetByIdCheckUser(long id);


}
