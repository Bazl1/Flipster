﻿using Microsoft.AspNetCore.Http;

namespace Flipster.Modules.Images.Contracts;

public interface IImageService
{
    Task<string> LoadImageAsync(IFormFile image);
    Task<List<string>> LoadImagesAsync(IFormFileCollection images);
    Task RemoveImageAsync(string url);
    Task RemoveImagesAsync(List<string> urls);
}