using FluentValidation;
using Soft_furniture.Service.Dtos.Auth;

namespace Soft_furniture.Service.Validators.Dtos.Auth;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(dto => dto.PhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
            .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");

        RuleFor(dto => dto.Password).Must(password => PasswordValidator.IsStrongPasswordd(password).IsValid)
            .WithMessage("Password is not strong password!");
    }
}
