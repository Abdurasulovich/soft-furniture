using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Caching.Memory;
using Soft_furniture.DataAccess.Interfaces.Catalogs;
using Soft_furniture.DataAccess.Interfaces.Types;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.DataAccess.ViewModels.Furniture_Types;
using Soft_furniture.Domain.Entities.Furniture_Type;
using Soft_furniture.Domain.Exceptions.Catalog;
using Soft_furniture.Domain.Exceptions.Files;
using Soft_furniture.Domain.Exceptions.Type;
using Soft_furniture.Service.Common.Helpers;
using Soft_furniture.Service.Dtos.Types;
using Soft_furniture.Service.Interfaces.Common;
using Soft_furniture.Service.Interfaces.Furniture_types;
using System.Runtime.CompilerServices;

namespace Soft_furniture.Service.Services.Furniture_types;

public class TypeService : ITypeService
{
    private readonly ITypeRepository _typeRepository;
    private readonly IFileService _fileService;
    private readonly ICatalogRepository _catalogRepository;
    private readonly IMemoryCache _memoryCache;
    private const int CACHED_SECONDS = 30;

    public TypeService(ITypeRepository typeRepository,
        IFileService fileService, IMemoryCache memoryCache,
        ICatalogRepository catalogRepository)
    {
        this._typeRepository = typeRepository;
        this._fileService = fileService;
        this._memoryCache = memoryCache;
        this._catalogRepository = catalogRepository;
    }
    public async Task<long> CountAsync() => await _typeRepository.CountAsync();

    public async Task<bool> CreateAsync(TypeCreateDto dto)
    {
        var type = await _typeRepository.GetByNameAsync(dto.Name);
        if (type != null) throw new AlreadyExistException();
        else
        {
            string imagepath = await _fileService.UploadImageAsync(dto.ImagePath);
            Furniture_Type types = new Furniture_Type()
            {
                Name = dto.Name,
                FurnitureCatalogId = dto.FurnitureCatalogId,
                ImagePath = imagepath,
                Description = dto.Description,
                CreatedAt = TimeHelper.GetDateTime(),
                UpdatedAt = TimeHelper.GetDateTime()
            };
            var result = await _typeRepository.CreateAsync(types);
            return result > 0;
        }
    }

    public async Task<bool> DeleteAsync(long typeId)
    {
        var type = await _typeRepository.GetByIdAsync(typeId);
        if (type is null) throw new CatalogNotFoundExeption();

        var result = await _fileService.DeleteImageAsync(type.ImagePath);
        if (result == false) throw new ImageNotFoundException();

        var dbResult = await _typeRepository.DeleteAsync(typeId);
        return dbResult > 0;
    }

    public async Task<IList<Furniture_Type>> GetAllByCatalogIdAsync(long catalogId)
    {
        var catalog = await _catalogRepository.GetByIdAsync(catalogId);
        if (catalog is null) throw new CatalogNotFoundExeption();
        var type = await _typeRepository.GetAllByCatalogIdAsync(catalogId);
        return type;
    }

    public async Task<Furniture_Type> GetByIdAsync(long typeId)
    {
        if (_memoryCache.TryGetValue(typeId, out Furniture_Type cachedType))
        {
            return cachedType!;
        }
        else
        {
            var type = await _typeRepository.GetByIdAsync(typeId);
            if (type is null) throw new TypeNotFoundException();

            _memoryCache.Set(typeId, type, TimeSpan.FromSeconds(CACHED_SECONDS));
            return type;
        }
    }

    public async Task<bool> UpdateAsync(long typeId, TypeUpdateDto dto)
    {
        var catalog = await _catalogRepository.GetByIdAsync(dto.FurnitureCatalogId);
        if (catalog is null) throw new CatalogNotFoundExeption();
        var type = await _typeRepository.GetByIdAsync(typeId);
        if (type is null) throw new TypeNotFoundException();

        //parse new items to type
        type.Name = dto.Name;
        type.FurnitureCatalogId = dto.FurnitureCatalogId;
        type.Description = dto.Description;

        if (dto.ImagePath is not null)
        {
            //delete old image
            var deleteResult = await _fileService.DeleteImageAsync(type.ImagePath);
            if (deleteResult is false) throw new ImageNotFoundException();

            //upload new image
            string newImagePath = await _fileService.UploadImageAsync(dto.ImagePath);

            //parse new path to type
            type.ImagePath = newImagePath;
        }
        //else type old image have to save

        type.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _typeRepository.UpdateAsync(typeId, type);
        return dbResult > 0;
    }
}
