using Microsoft.Extensions.Caching.Memory;
using Soft_furniture.DataAccess.Interfaces.Catalogs;
using Soft_furniture.DataAccess.Utils;
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
    private readonly IMemoryCache _memoryCache;
    private const int CACHED_SECONDS = 30;

    public CatalogService(ICatalogRepository catalogRepository,
        IFileService fileService, IMemoryCache memoryCache)
    {
        this._catalogRepository = catalogRepository;
        this._fileService = fileService;
        this._memoryCache = memoryCache;
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

    public async Task<IList<Catalog>> GetAllAsync(PaginationParams @params)
    {
        var catalog = await _catalogRepository.GetAllAsync(@params);
        return catalog;
    }

    public async Task<Catalog> GetByIdAsync(long catalogId)
    {
        if (_memoryCache.TryGetValue(catalogId, out Catalog cachedCatalog))
        {
            return cachedCatalog!;
        }
        else
        {
            var catalog = await _catalogRepository.GetByIdAsync(catalogId);
            if (catalog is null) throw new CatalogNotFoundExeption();

            _memoryCache.Set(catalogId, catalog, TimeSpan.FromSeconds(CACHED_SECONDS));
            return catalog;
        }
    }

    public async Task<bool> UpdateAsync(long catalogId, CatalogUpdateDto dto)
    {
        var catalog = await _catalogRepository.GetByIdAsync(catalogId);
        if (catalog is null) throw new CatalogNotFoundExeption();

        //parse new items to catalog
        catalog.Name = dto.Name;

        if (dto.ImagePath is not null)
        {
            //delete old image
            var deleteResult = await _fileService.DeleteImageAsync(catalog.ImagePath);
            if (deleteResult is false) throw new ImageNotFoundException();

            //upload new image
            string newImagePath = await _fileService.UploadImageAsync(dto.ImagePath);

            //parse new path to catalog
            catalog.ImagePath = newImagePath;
        }
        //else catalog old image have to save

        catalog.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _catalogRepository.UpdateAsync(catalogId, catalog);
        return dbResult > 0;
    }
}
