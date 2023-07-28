namespace Soft_furniture.Domain.Exceptions.Delivers;

public class DeliverCacheDataExpiredException : ExpiredException
{
    public DeliverCacheDataExpiredException()
    {
        TitleMessage = "Deliver data has expired!";
    }
}