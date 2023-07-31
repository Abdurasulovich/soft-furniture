using Microsoft.AspNetCore.Mvc;
using Soft_furniture.Service.Validators;

namespace Soft_furniture.UnitTest.ValidatorTests;

public class PhoneNumberValidatorTest
{
    [Theory]
    [InlineData("+998913774506")]
    [InlineData("+998995464506")]
    [InlineData("+998903774506")]
    [InlineData("+998506001240")]
    public void ShouldReturenCorrect(string phone)
    {
        var result = PhoneNumberValidator.IsValid(phone);
        Assert.True(result);
    }
    [Theory]
    [InlineData("+99891377450")]
    [InlineData("99891377450")]
    [InlineData("998913774506")]
    [InlineData("+98913774506")]
    [InlineData("+9991377450")]
    [InlineData("+998913 77450")]
    [InlineData("daknvlkan kja")]
    [InlineData("+99B9163774506")]
    [InlineData("+9999163774506")]
    [InlineData("+999163774506")]
    [InlineData("+99916377450 ")]
    public void ShouldRetureWrong(string phone)
    {
        var result = PhoneNumberValidator.IsValid(phone);
        Assert.False(result);
    }
}
