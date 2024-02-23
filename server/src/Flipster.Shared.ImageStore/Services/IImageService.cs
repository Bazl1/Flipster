using Microsoft.AspNetCore.Http;

namespace Flipster.Shared.ImageStore.Services;

public interface IImageService
{
    Task<IEnumerable<string>> LoadImagesAsync(IFormFileCollection images);
    Task<string> LoadImageAsync(IFormFile image);
    Task RemoveImagesAsync(IEnumerable<string> urls);
    Task RemoveImageAsync(string url);
}
