using Microsoft.AspNetCore.Mvc;
using Soft_furniture.DataAccess.Interfaces.Catalogs;
using Soft_furniture.DataAccess.Interfaces.Types;
using Soft_furniture.DataAccess.Repositories.Catalogs;
using Soft_furniture.DataAccess.Utils;
using Soft_furniture.Domain.Entities.Furniture_Catalog;
using Soft_furniture.Domain.Entities.Furniture_Type;
using Soft_furniture.Domain.Exceptions.Catalog;
using Soft_furniture.Domain.Exceptions.Files;
using Soft_furniture.Domain.Exceptions.Type;
using Soft_furniture.Service.Common.Helpers;
using Soft_furniture.Service.Dtos.Types;
using Soft_furniture.Service.Interfaces.Common;
using Soft_furniture.Service.Interfaces.Furniture_types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soft_furniture.Service.Services.Furniture_types;

public class TypeService : ITypeService
{
    private readonly ITypeRepository _typeRepository;
    private readonly IFileService _fileService;

    public TypeService(ITypeRepository typeRepository,
        IFileService fileService)
    {
        this._typeRepository = typeRepository;
        this._fileService = fileService;
    }
    public async Task<long> CountAsync() => await _typeRepository.CountAsync();

    public async Task<bool> CreateAsync(TypeCreateDto dto)
    {
        string imagepath = await _fileService.UploadImageAsync(dto.ImagePath);
        Furniture_Type type = new Furniture_Type()
        {
            Name = dto.Name,
            ImagePath = imagepath,
            Description = dto.Description,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };
        var result = await _typeRepository.CreateAsync(type);
        return result > 0;
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

    public async Task<IList<Furniture_Type>> GetAllAsync(PaginationParams @params)
    {
        var type = await _typeRepository.GetAllAsync(@params);
        return type;
    }

    public async Task<Furniture_Type> GetByIdAsync(long typeId)
    {
        var type = await _typeRepository.GetByIdAsync(typeId);
        if (type is null) throw new TypeNotFoundException();
        else return type;
    }

    public async Task<bool> UpdateAsync(long typeId, TypeUpdateDto dto)
    {
        var type = await _typeRepository.GetByIdAsync(typeId);
        if (type is null) throw new CatalogNotFoundExeption();

        //parse new items to type
        type.Name = dto.Name;
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
