using FluentValidation;
using FluentValidation.Validators;
using Soft_furniture.Service.Common.Helpers;
using Soft_furniture.Service.Dtos.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soft_furniture.Service.Validators.Dtos.Catalogs;

public class CatalogUpdateValidator : AbstractValidator<CatalogUpdateDto>
{
    private short imgSize = 5;
    public CatalogUpdateValidator()
    {
        RuleFor(dto => dto.Name).NotNull().NotEmpty().WithMessage("Name field is required!")
            .MinimumLength(3).WithMessage("Name must be more than 3 characters!")
            .MaximumLength(50).WithMessage("Name must be less than 50 characters!");

        When(dto => dto.ImagePath is not null, () =>
        {
            RuleFor(dto => dto.ImagePath.Length).LessThan(imgSize * 1024 * 1024).WithMessage($"Image size must be less than {imgSize} MB!");
            RuleFor(dto => dto.ImagePath.FileName).Must(predicate =>
            {
                FileInfo fileInfo = new FileInfo(predicate);
                return MediaHelper.GetImageExtension().Contains(fileInfo.Extension);
            }).WithMessage("This file type is not required!");
        });
    }
}
