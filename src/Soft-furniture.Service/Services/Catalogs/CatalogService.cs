using Soft_furniture.DataAccess.Interfaces.Catalogs;
using Soft_furniture.Domain.Entities.Furniture_Catalog;
using Soft_furniture.Domain.Exceptions.Catalog;
using Soft_furniture.Domain.Exceptions.Files;
using Soft_furniture.Service.Common.Helpers;
using Soft_furniture.Service.Dtos.Catalogs;
using Soft_furniture.Service.Interfaces.Catalogs;
using Soft_furniture.Service.Interfaces.Common;

namespace Soft_furniture.Service.Services.Catalogs;

public class CatalogService : ICatalogService
{
    private readonly ICatalogRepository _catalogRepository;
    private readonly IFileService _fileService;

    public CatalogService(ICatalogRepository catalogRepository,
        IFileService fileService)
    {
        this._catalogRepository = catalogRepository;
        this._fileService = fileService;
    }

    public async Task<long> CountAsync() => await _catalogRepository.CountAsync();

    public async Task<bool> CreateAsync(CatalogCreateDto dto)
    {
        string imagepath = await _fileService.UploadImageAsync(dto.ImagePath);
        Catalog catalog = new Catalog()
        {
            ImagePath = imagepath,
            Name = dto.Name,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var result = await _catalogRepository.CreateAsync(catalog);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(long catalogId)
    {
        var catalog = await _catalogRepository.GetByIdAsync(catalogId);
        if (catalog is null) throw new CatalogNotFoundExeption();

        var result = await _fileService.DeleteImageAsync(catalog.ImagePath);
        if (result == false) throw new ImageNotFoundException();

        var dbResult = await _catalogRepository.DeleteAsync(catalogId);
        return dbResult > 0;
    }
}
