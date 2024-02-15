using Flipster.Modules.Images.Contracts;
using Flipster.Modules.Images.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Flipster.Modules.Images.Endpoints;

public static class ImagesEndpoints
{
    public static IEndpointRouteBuilder MapImagesEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/image", LoadImage)
            .DisableAntiforgery();
        builder.MapPost("/images", LoadImages)
            .DisableAntiforgery();
        
        builder.MapDelete("/image", RemoveImage)
            .DisableAntiforgery();
        builder.MapDelete("/images", RemoveImages)
            .DisableAntiforgery();
        
        return builder;
    }

    private static async Task<IResult> LoadImage(
        HttpContext context,
        IImageService imageService,
        IFormFile image)
    {
        try
        {
            return Results.Ok(await imageService.LoadImageAsync(image));
        }
        catch
        {
            return Results.BadRequest(new ErrorDto("An unexpected error occurred while loading the image."));
        }
    }
    
    private static async Task<IResult> LoadImages(
        HttpContext context,
        IImageService imageService,
        IFormFileCollection images)
    {
        try
        {
            return Results.Ok(await imageService.LoadImagesAsync(images));
        }
        catch
        {
            return Results.BadRequest(new ErrorDto("An unexpected error occurred while loading the image."));
        }
    }
    
    private static async Task<IResult> RemoveImage(
        HttpContext context,
        IImageService imageService,
        [FromBody] string url)
    {
        try
        {
            await imageService.RemoveImageAsync(url);
            return Results.Ok();
        }
        catch
        {
            return Results.BadRequest(new ErrorDto("An unexpected error occurred while loading the image."));
        }
    }

    private static async Task<IResult> RemoveImages(
        HttpContext context,
        IImageService imageService,
        [FromBody] List<string> urls)
    {
        try
        {
            await imageService.RemoveImagesAsync(urls);
            return Results.Ok();
        }
        catch
        {
            return Results.BadRequest(new ErrorDto("An unexpected error occurred while loading the image."));
        }
    }
}