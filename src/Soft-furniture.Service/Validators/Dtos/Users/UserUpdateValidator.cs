using FluentValidation;
using Soft_furniture.Service.Dtos.Users;

namespace Soft_furniture.Service.Validators.Dtos.Users;

public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateValidator()
    {

        RuleFor(dto => dto.FirstName).NotNull().NotEmpty().WithMessage("Name field is required!")
            .MinimumLength(3).WithMessage("Name must be more than 3 characters!")
            .MaximumLength(50).WithMessage("Name must be less than 50 characters!");

        RuleFor(dto => dto.LastName).NotNull().NotEmpty().WithMessage("LastName field is required!")
            .MinimumLength(3).WithMessage("Name must be more than 3 characters!")
            .MaximumLength(50).WithMessage("Name must be less than 50 characters!");

        RuleFor(dto => dto.PhoneNumber).NotNull().NotEmpty().WithMessage("PhoneNumber field is required!")
            .MinimumLength(3).WithMessage("Name must be more than 3 characters!")
            .MaximumLength(50).WithMessage("Name must be less than 50 characters!");

        RuleFor(dto => dto.PasswordHash).Must(password => PasswordValidator.IsStrongPasswordd(password).IsValid)
            .WithMessage("Password is not strong password!");

        RuleFor(dto => dto.Country).NotNull().NotEmpty().WithMessage("Country is required!")
            .MaximumLength(40).WithMessage("Country must be less than 40 character!")
            .MinimumLength(2).WithMessage("Country must be more than 2 characters!");

        RuleFor(dto => dto.Region).NotNull().NotEmpty().WithMessage("Country is required!")
            .MaximumLength(50).WithMessage("Region must be less than 50 character!")
            .MinimumLength(4).WithMessage("Region must be more than 4 characters!");

        RuleFor(dto => dto.City).NotNull().NotEmpty().WithMessage("Country is required!")
            .MaximumLength(40).WithMessage("City must be less than 40 character!")
            .MinimumLength(2).WithMessage("City must be more than 2 characters!");

        RuleFor(dto => dto.Address).NotNull().NotEmpty().WithMessage("Country is required!")
            .MaximumLength(50).WithMessage("Address must be less than 50 character!")
            .MinimumLength(5).WithMessage("Address must be more than 5 characters!");
    }
}
