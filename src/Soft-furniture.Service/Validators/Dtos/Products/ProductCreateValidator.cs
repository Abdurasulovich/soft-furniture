using FluentValidation;
using Soft_furniture.Service.Common.Helpers;
using Soft_furniture.Service.Dtos.Products;

namespace Soft_furniture.Service.Validators.Dtos.Products;

public class ProductCreateValidator : AbstractValidator<ProductsCreateDto>
{
    private short imgSize = 20;

    public ProductCreateValidator()
    {
        RuleFor(dto => dto.Name).NotNull().NotEmpty().WithMessage("Name field is required!")
            .MinimumLength(3).WithMessage("Name must be more than 3 characters!")
            .MaximumLength(50).WithMessage("Name must be less than 50 characters!");


        RuleFor(dto => dto.ImagePath).NotEmpty().NotNull().WithMessage("Image filed is required!");
        RuleFor(dto => dto.ImagePath.Length).LessThan(imgSize * 1024 * 1024).WithMessage($"Image size must be less than {imgSize} MB!");
        RuleFor(dto => dto.ImagePath.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);
            return MediaHelper.GetImageExtension().Contains(fileInfo.Extension);
        }).WithMessage("This file type is not required!");


        RuleFor(dto => dto.FurnitureTypeId).NotNull().NotEmpty().WithMessage("Furniture catalog id is required!");


        RuleFor(dto => dto.UnitPrice).NotNull().NotEmpty().WithMessage("Product price is required!");


        RuleFor(dto => dto.Description).NotNull().NotEmpty().WithMessage("Description field is required!")
            .MinimumLength(20).WithMessage("Description text must be more than 20 characters!");
    }
}
