using Microsoft.Extensions.Caching.Memory;
using Soft_furniture.DataAccess.Interfaces.Products;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.Domain.Entities.Products;
using Soft_furniture.Domain.Exceptions.Catalog;
using Soft_furniture.Domain.Exceptions.Files;
using Soft_furniture.Domain.Exceptions.Product;
using Soft_furniture.Domain.Exceptions.Type;
using Soft_furniture.Service.Common.Helpers;
using Soft_furniture.Service.Dtos.Products;
using Soft_furniture.Service.Interfaces.Common;
using Soft_furniture.Service.Interfaces.Furniture_types;
using Soft_furniture.Service.Interfaces.Products;

namespace Soft_furniture.Service.Services.Products;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IFileService _fileService;
    private readonly ITypeService _typeService;
    private readonly IMemoryCache _memoryCache;
    private const int CACHED_SECONDS = 30;

    public ProductService(IProductRepository productRepository,
        IFileService fileService, IMemoryCache memoryCache, ITypeService typeService)
    {
        this._productRepository = productRepository;
        this._fileService = fileService;
        this._memoryCache = memoryCache;
        this._typeService = typeService;
    }
    public async Task<long> CountAsync() => await _productRepository.CountAsync();

    public async Task<bool> CreateAsync(ProductsCreateDto dto)
    {
        var products = await _productRepository.GetAllByProductNameAsync(dto.Name);
        if (products is null)
        {
            var typeId = await _typeService.GetByIdAsync(dto.FurnitureTypeId);
            if (typeId is null) throw new TypeNotFoundException();
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
        }else
        {
            throw new ProductAlreadyExsistException();
        }
    }

    public async Task<bool> DeleteAsync(long productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null) throw new TypeNotFoundException();

        var result = await _fileService.DeleteImageAsync(product.ImagePath);
        if (result == false) throw new ImageNotFoundException();

        var dbResult = await _productRepository.DeleteAsync(productId);
        return dbResult > 0;
    }

    public async Task<IList<Product>> GetAllByTypeIdAsync( long typeId, PaginationParams @params)
    {
        var type = await _typeService.GetByIdAsync(typeId);
        if (type is null) throw new TypeNotFoundException();
        else
        {
            var product = await _productRepository.GetAllByTypeIdAsync(typeId, @params);
            return product;
        }
    }

    public async Task<Product> GetByIdAsync(long productId)
    {
        if (_memoryCache.TryGetValue(productId, out Product cachedProduct))
        {
            return cachedProduct!;
        }
        else
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product is null) throw new ProductNotFoundException();

            _memoryCache.Set(productId, product, TimeSpan.FromSeconds(CACHED_SECONDS));
            return product;
        }
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
