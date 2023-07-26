using Soft_furniture.Service.Dtos.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soft_furniture.Service.Interfaces.Notifications;

public interface ISmsSender
{
    public Task<bool> SendAsync(SmsMessage smsMessage);
}
