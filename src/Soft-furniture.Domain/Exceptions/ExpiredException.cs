using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Soft_furniture.Domain.Exceptions;

public class ExpiredException : Exception
{
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotFound;

    public string TitleMessage { get; protected set; } = String.Empty;
}
