﻿using Microsoft.AspNetCore.Http;

namespace Soft_furniture.Service.Interfaces.Common;

public interface IFileService
{
    //returns sub path of this image
    public Task<string> UploadImageAsync(IFormFile image);

    public Task<bool> DeleteImageAsync(string subpath);
    //returns sub path of this image
    public Task<string> UploadAvatarAsync(IFormFile avatar);

    public Task<bool> DeleteAvatarAsync(string subpath);

}
