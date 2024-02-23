using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Flipster.Shared.ImageStore.Services;

public class ImageService(
    IWebHostEnvironment _webHostEnvironment) : IImageService
{
    public const string BASE_URL = "http://localhost:5145/images/";

    public async Task<string> LoadImageAsync(IFormFile image)
    {
        var fileName = Path.Combine($"{Guid.NewGuid().ToString()}{Path.GetExtension(image.FileName)}");
        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
            await image.CopyToAsync(stream);
        return $"{BASE_URL}{fileName}";
    }

    public async Task<IEnumerable<string>> LoadImagesAsync(IFormFileCollection images)
    {
        var filePaths = new List<string>();
        foreach (var image in images)
            filePaths.Add(await LoadImageAsync(image));
        return filePaths;
    }

    public async Task RemoveImageAsync(string url)
    {
        var fileName = url.Replace(BASE_URL, "");
        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);
        if (!File.Exists(filePath))
            throw new Exception("File is not exists.");
        File.Delete(filePath);
    }

    public async Task RemoveImagesAsync(IEnumerable<string> urls)
    {
        foreach (var url in urls)
            await RemoveImageAsync(url);
    }
}
