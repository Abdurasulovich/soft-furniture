using System.Net;

namespace Soft_furniture.Domain.Exceptions;

public class NotFoundExeption : Exception
{
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotFound;

    public string TitleMessage { get; protected set; } = String.Empty;
}
