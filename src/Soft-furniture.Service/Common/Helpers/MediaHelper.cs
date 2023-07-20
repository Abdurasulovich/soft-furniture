namespace Soft_furniture.Service.Common.Helpers;

public class MediaHelper
{
    public static string MakeImageName(string imageName)
    {
        FileInfo fileInfo = new FileInfo(imageName);
        string extention = fileInfo.Extension;
        string name = "IMG_" + Guid.NewGuid().ToString() + extention;
        return name;
    }
}
