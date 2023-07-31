using FluentValidation;
using Soft_furniture.Service.Dtos.Orders;

namespace Soft_furniture.Service.Validators.Dtos.Orders;

public class OrderUpdateValidator : AbstractValidator<OrderUpdateDto>
{
    public OrderUpdateValidator()
    {
        RuleFor(dto => dto.ProductId).NotNull().NotEmpty()
            .WithMessage("ProductId field is required!");

        RuleFor(dto => dto.Latitude).NotNull().NotEmpty()
            .WithMessage("Latitude field is required!");

        RuleFor(dto => dto.Longitude).NotNull().NotEmpty()
            .WithMessage("Longitude field is required!");

        RuleFor(dto => dto.PaymentType).NotNull().NotEmpty()
            .WithMessage("PaymentType field is required!");



    }
}
