using Microsoft.AspNetCore.Http;
using Soft_furniture.Service.Dtos.Catalogs;
using Soft_furniture.Service.Validators.Dtos.Catalogs;
using System.Text;

namespace Soft_furniture.UnitTest.ValidatorTests;

public class CatalogCreateVlidatorTest
{
    [Theory]
    [InlineData("file.png")]
    [InlineData("file.jpg")]
    [InlineData("file.jpeg")]
    [InlineData("file.bmp")]
    public void ShouldReturnCorrectImageFileExtention(string imagename)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", comes from a line in section 1.10.32.\r\n\r\nThe standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from \"de Finibus Bonorum et Malorum\" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.");
        IFormFile file = new FormFile(new MemoryStream(byteImage), 0, byteImage.Length, "data", imagename);
        CatalogCreateDto catalogCreateDto = new CatalogCreateDto()
        {
            Name = "nimadur products",
            ImagePath = file
        };
        var validator = new CatalogCreateValidator();
        var result = validator.Validate(catalogCreateDto);
        Assert.True(result.IsValid);
        
    }

    [Theory]
    [InlineData("file.zip")]
    [InlineData("file.mp3")]
    [InlineData("file.html")]
    [InlineData("file.css")]
    [InlineData("file.gif")]
    [InlineData("file.txt")]
    [InlineData("file.mp4")]
    [InlineData("file.avi")]
    [InlineData("file.mvk")]
    [InlineData("file.waw")]
    [InlineData("file.webmp")]
    [InlineData("file.pdf")]
    [InlineData("file.doc")]
    [InlineData("file.docx")]
    public void ShouldReturnWrongImageFileExtention(string imagename)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", comes from a line in section 1.10.32.\r\n\r\nThe standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from \"de Finibus Bonorum et Malorum\" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.");
        IFormFile file = new FormFile(new MemoryStream(byteImage), 0, byteImage.Length, "data", imagename);
        CatalogCreateDto catalogCreateDto = new CatalogCreateDto()
        {
            Name = "nimadur products",
            ImagePath = file
        };
        var validator = new CatalogCreateValidator();
        var result = validator.Validate(catalogCreateDto);
        Assert.False(result.IsValid);

    }

    [Theory]
    [InlineData(10.5)]
    [InlineData(20.01)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(6)]
    [InlineData(8)]
    [InlineData(8.5)]
    [InlineData(7)]
    [InlineData(7.4)]
    public void ShouldReturnWrongImageFileSize(double ImageSizeMb)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", comes from a line in section 1.10.32.\r\n\r\nThe standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from \"de Finibus Bonorum et Malorum\" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.");
        long ImageSizeInMb =(long) ImageSizeMb * 1024 * 1024;
        IFormFile file = new FormFile(new MemoryStream(byteImage), 0, ImageSizeInMb,  "data", "file.png");
        CatalogCreateDto catalogCreateDto = new CatalogCreateDto()
        {
            Name = "nimadur products",
            ImagePath = file
        };
        var validator = new CatalogCreateValidator();
        var result = validator.Validate(catalogCreateDto);
        Assert.False(result.IsValid);

    }
}
