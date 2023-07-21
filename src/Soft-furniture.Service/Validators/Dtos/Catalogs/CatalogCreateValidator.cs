using FluentValidation;
using Soft_furniture.Service.Dtos.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soft_furniture.Service.Validators.Dtos.Catalogs;

public class CatalogCreateValidator : AbstractValidator<CatalogCreateDto>
{
    public CatalogCreateValidator()
    {
        RuleFor(dto=>dto.Name).NotNull().NotEmpty().WithMessage("Name field is required!")
            .MinimumLength(3).WithMessage("Name must be more than 2 characters!")
            .MaximumLength(50).WithMessage("Name must be less than 50 characters!")
    }
}
