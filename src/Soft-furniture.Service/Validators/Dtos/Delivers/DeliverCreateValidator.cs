using FluentValidation;
using Soft_furniture.Service.Dtos.Delivers;

namespace Soft_furniture.Service.Validators.Dtos.Delivers;

public class DeliverCreateValidator : AbstractValidator<DeliverCreateDto>
{
    public DeliverCreateValidator()
    {

        RuleFor(dto => dto.BirthDate).NotNull().NotEmpty().WithMessage("BirthDate is required!");

        RuleFor(dto => dto.Description).NotNull().NotEmpty().WithMessage("Description field is required!")
            .MinimumLength(20).WithMessage("Description text must be more than 20 characters!");

        RuleFor(dto => dto.IsMale).NotNull().NotEmpty()
            .WithMessage("Gender is required");

        RuleFor(dto => dto.FirstName).NotNull().NotEmpty().WithMessage("Firstname is required!")
            .MaximumLength(30).WithMessage("Firstname must be less than 30 characters");

        RuleFor(dto => dto.LastName).NotNull().NotEmpty().WithMessage("Lastname is required!")
            .MaximumLength(30).WithMessage("Lastname must be less than 30 characters");

        RuleFor(dto => dto.PhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
            .WithMessage("Phone number is invalid! ex: +998ccYYYAABB");

        RuleFor(dto => dto.PasswordHash).Must(password => PasswordValidator.IsStrongPasswordd(password).IsValid)
            .WithMessage("Password is not strong password!");

        RuleFor(dto => dto.PasspordSeriaNumber).Must(passport => PassportSeriaNumberValidator.IsTruePassportSeriaNumber(passport).IsValid)
            .WithMessage("PasspordSeriaNumber is required!");

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
