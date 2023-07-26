using Soft_furniture.DataAccess.Interfaces.Catalogs;
using Soft_furniture.DataAccess.Interfaces.Products;
using Soft_furniture.DataAccess.Repositories.Catalogs;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.Domain.Entities.Furniture_Catalog;
using Soft_furniture.Domain.Entities.Products;
using Soft_furniture.Domain.Exceptions.Catalog;
using Soft_furniture.Domain.Exceptions.Files;
using Soft_furniture.Domain.Exceptions.Product;
using Soft_furniture.Service.Common.Helpers;
using Soft_furniture.Service.Dtos.Products;
using Soft_furniture.Service.Interfaces.Common;
using Soft_furniture.Service.Interfaces.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soft_furniture.Service.Services.Products;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IFileService _fileService;

    public ProductService(IProductRepository productRepository,
        IFileService fileService)
    {
        this._productRepository = productRepository;
        this._fileService = fileService;
    }
    public async Task<long> CountAsync() => await _productRepository.CountAsync();

    public async Task<bool> CreateAsync(ProductsCreateDto dto)
    {
        string imagepath = await _fileService.UploadImageAsync(dto.ImagePath);
        Product product = new Product()
        {
            Name = dto.Name,
            ImagePath = imagepath,
            Description = dto.Description,
            FurnitureTypeId = dto.FurnitureTypeId,
            UnitPrice = dto.UnitPrice,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var result = await _productRepository.CreateAsync(product);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null) throw new CatalogNotFoundExeption();

        var result = await _fileService.DeleteImageAsync(product.ImagePath);
        if (result == false) throw new ImageNotFoundException();

        var dbResult = await _productRepository.DeleteAsync(productId);
        return dbResult > 0;
    }

    public async Task<IList<Product>> GetAllByTypeIdAsync(long productId, PaginationParams @params)
    {
        var product = await _productRepository.GetAllByTypeIdAsync(productId, @params);
        return product;
    }

    public async Task<Product> GetByIdAsync(long productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null) throw new ProductNotFoundException();
        else return product;
    }

    public async Task<(int ItemsCount, IList<Product>)> SearchAsync(string search, PaginationParams @params)
    {
        var product = await _productRepository.SearchAsync(search, @params);
        return product;
    }

    public async Task<bool> UpdateAsync(long id, ProductsUpdateDto dto)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null) throw new ProductNotFoundException();

        //parse new items to catalog
        product.Name = dto.Name;
        product.Description = dto.Description;
        product.UnitPrice = dto.UnitPrice;
        product.FurnitureTypeId = dto.FurnitureTypeId;

        if (dto.ImagePath is not null)
        {
            //delete old image
            var deleteResult = await _fileService.DeleteImageAsync(product.ImagePath);
            if (deleteResult is false) throw new ImageNotFoundException();

            //upload new image
            string newImagePath = await _fileService.UploadImageAsync(dto.ImagePath);

            //parse new path to catalog
            product.ImagePath = newImagePath;
        }
        //else catalog old image have to save

        product.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _productRepository.UpdateAsync(id, product);
        return dbResult > 0;
    }
}
