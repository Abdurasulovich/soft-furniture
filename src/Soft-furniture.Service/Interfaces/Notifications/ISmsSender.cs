using Soft_furniture.Service.Dtos.Notifications;

namespace Soft_furniture.Service.Interfaces.Notifications;

public interface ISmsSender
{
    public Task<bool> SendAsync(SmsMessage smsMessage);
}
