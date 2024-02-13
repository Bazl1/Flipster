using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Flipster.Modules.Images.Contracts;

internal class ImageService(
    IWebHostEnvironment _webHostEnvironment) : IImageService
{
    public const string BASE_URL = "http://localhost:5247/";
    
    public async Task<string> LoadImageAsync(IFormFile image)
    {
        var fileName = Path.Combine($"{Guid.NewGuid().ToString()}.{Path.GetExtension(image.FileName)}");
        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
            await image.CopyToAsync(stream);
        return $"{BASE_URL}{fileName}";
    }

    public async Task<List<string>> LoadImagesAsync(List<IFormFile> images)
    {
        var filePaths = new List<string>();
        foreach (var image in images)
            filePaths.Add(await LoadImageAsync(image));
        return filePaths;
    }

    public async Task RemoveImageAsync(string url)
    {
        var fileName = url.Replace(BASE_URL, "");
        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);
        if (!File.Exists(filePath))
            throw new Exception("File is not exists.");
        File.Delete(filePath);
    }

    public async Task RemoveImagesAsync(List<string> urls)
    {
        foreach (var url in urls)
            await RemoveImageAsync(url);
    }
}