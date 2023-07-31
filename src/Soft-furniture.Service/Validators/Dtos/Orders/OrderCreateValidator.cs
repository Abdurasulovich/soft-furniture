using FluentValidation;
using Soft_furniture.Service.Dtos.Delivers;
using Soft_furniture.Service.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soft_furniture.Service.Validators.Dtos.Orders
{
    public class OrderCreateValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateValidator()
        {

            RuleFor(dto => dto.ProductId).NotNull().NotEmpty()
                .WithMessage("Product Id is required!");

            RuleFor(dto => dto.Latitude).NotNull().NotEmpty()
                .WithMessage("Latitude is required");

            RuleFor(dto => dto.Longitude).NotNull().NotEmpty()
                .WithMessage("Longitude is required");

            RuleFor(dto => dto.PaymentType).NotNull().NotEmpty()
                .WithMessage("Payment type is required!");

        }
    }
}
