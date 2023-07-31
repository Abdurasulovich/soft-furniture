using Soft_furniture.Service.Validators;

namespace Soft_furniture.UnitTest.ValidatorTests;

public class PasswordValidatorTests
{
    [Theory]
    [InlineData("AAaa@@11")]
    [InlineData("Ac035465135146384!")]
    [InlineData("fdknglkdsjfbnlkj468J4!#$%dsljfbiu")]
    [InlineData("dskjah684kleuhf#$kasjbKJBGSK")]
    [InlineData("BBbb##43132%")]
    [InlineData("JJkjl__adsjk-46")]
    public void IsStrongPassword(string password)
    {
        var result = PasswordValidator.IsStrongPasswordd(password);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("AAaa@@1")]
    [InlineData("AAaa!!@@")]
    [InlineData("aaaa@@11")]
    [InlineData("AAAA@@11")]
    [InlineData("AAAA@@aa")]
    [InlineData("AAAAgg22aa")]
    [InlineData("AAaa222 ")]
    [InlineData("BBBBBBBBBB")]
    [InlineData("qqqqqqqqqqq")]
    [InlineData("9999999999")]
    [InlineData("@#$%$#@")]
    public void ShouldReturnWeakPassword(string password)
    {
        var result = PasswordValidator.IsStrongPasswordd(password);
        Assert.False(result.IsValid);
    }
}
