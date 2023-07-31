using System.Net;

namespace Soft_furniture.Domain.Exceptions;

public class NotFoundExeption : ClientException
{
    public override HttpStatusCode StatusCode { get; } = HttpStatusCode.NotFound;

    public override string TitleMessage { get; protected set; } = String.Empty;
}
