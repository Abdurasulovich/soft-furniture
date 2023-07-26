using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Soft_furniture.Service.Dtos.Notifications
{
    public class SmsMessage
    {
        public string PhoneNumber { get; set; } = String.Empty;

        public string Content { get; set; } = String.Empty;
    }
}
