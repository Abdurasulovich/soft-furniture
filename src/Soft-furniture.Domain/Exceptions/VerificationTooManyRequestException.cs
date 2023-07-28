namespace Soft_furniture.Domain.Exceptions;

public class VerificationTooManyRequestException : TooManyRequestException
{
    public VerificationTooManyRequestException()
    {
        TitleMessage = "You tried more than limit!";
    }
}
