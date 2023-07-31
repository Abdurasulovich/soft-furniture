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

    public static string[] GetImageExtension()
    {
        return new string[]
        {
            //JPG files
            ".jpg", ".jpeg",
            //Png files
            ".png",
            //Bmp files
            ".bmp",

        };
    }
}
