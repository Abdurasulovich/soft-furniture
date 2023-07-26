using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soft_furniture.Service.Dtos.Security;

public class VerificationDto
{
    public int Code { get; set; }

    public int Attempt { get; set; }

    public DateTime CreatedAt { get; set; }
}
