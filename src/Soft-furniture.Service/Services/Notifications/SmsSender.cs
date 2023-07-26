using Soft_furniture.Service.Dtos.Notifications;
using Soft_furniture.Service.Interfaces.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soft_furniture.Service.Services.Notifications;

public class SmsSender : ISmsSender
{
    public Task<bool> SendAsync(SmsMessage smsMessage)
    {
        throw new NotImplementedException();
    }
}
