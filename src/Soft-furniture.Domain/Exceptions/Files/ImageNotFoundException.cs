namespace Soft_furniture.Domain.Exceptions.Files;

public class ImageNotFoundException : NotFoundExeption
{
    public ImageNotFoundException()
    {
        this.TitleMessage = "Image not found!";
    }
}
