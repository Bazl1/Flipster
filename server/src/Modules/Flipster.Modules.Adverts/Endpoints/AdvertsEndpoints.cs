using System.Security.Claims;
using AutoMapper;
using Flipster.Modules.Adverts.Data;
using Flipster.Modules.Adverts.Dto;
using Flipster.Modules.Adverts.Entities;
using Flipster.Modules.Adverts.Enums;
using Flipster.Modules.Adverts.Services;
using Flipster.Modules.Identity.Domain.User.Entities.Contracts.Abstractions;
using Flipster.Modules.Images.Contracts;
using Flipster.Modules.Images.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Flipster.Modules.Adverts.Endpoints;

public static class AdvertsEndpoints
{
    public static IEndpointRouteBuilder MapAdvertsEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/", Create).RequireAuthorization();
        builder.MapDelete("/{id}", Delete).RequireAuthorization();
        builder.MapPut("/{id}", Update).RequireAuthorization();

        builder.MapGet("/{id}", GetById);
        builder.MapGet("/", GetAll);

        return builder;
    }

    private static async Task<IResult> GetById(
        HttpContext context,
        AdvertsDbContext db,
        IUserModule userModule,
        IMapper mapper,
        [FromRoute] string id)
    {
        if (await db.Adverts.Include(advert => advert.Category).SingleOrDefaultAsync(a => a.Id == id) is not Advert advert)
            return Results.BadRequest(new ErrorDto("Advert with given id is not found."));
        var result = mapper.Map<AdvertDto>(advert);
        var seller = userModule.GetById(advert.SellerId);
        result.Contact.Name = seller.Name;
        result.Contact.Avatar = seller.Avatar;
        return Results.Ok();
    }

    private static async Task<IResult> Update(
        HttpContext context,
        AdvertsDbContext db,
        IMapper mapper,
        IImageService imageService,
        [FromRoute] string id,
        [FromForm] AdvertUpdateRequest request)
    {
        if (await db.Adverts.SingleOrDefaultAsync(a => a.Id == id) is not Advert advert)
            return Results.BadRequest(new ErrorDto("Advert with given id is not found."));
        advert.Title = request.Title;
        advert.Description = request.Description;
        advert.CategoryId = request.CategoryId;
        advert.IsFree = request.IsFree;
        advert.Price = request.Price;
        advert.ProductType = Enum.Parse<AdvertProductType>(request.ProductType);
        advert.BusinessType = Enum.Parse<AdvertBusinessType>(request.BusinessType);
        advert.Location = request.Location;
        advert.Email = request.Email;
        advert.PhoneNumber = request.PhoneNumber;
        try
        {
            foreach (var image in advert.Images)
            {
                if (!request.ImageUrls.Contains(image))
                {
                    await imageService.RemoveImageAsync(image);
                    advert.Images.Remove(image);
                }
            }
            advert.Images.AddRange(await imageService.LoadImagesAsync(request.Images));
        }
        catch
        {
            return Results.BadRequest(new ErrorDto("An unexpected error occurred while loading the image."));
        }

        await db.SaveChangesAsync();
        return Results.Ok(mapper.Map<AdvertDto>(advert));
    }

    private static async Task<IResult> Delete(
        HttpContext context,
        AdvertsDbContext db,
        IImageService imageService,
        [FromRoute] string id)
    {
        if (await db.Adverts.SingleOrDefaultAsync(a => a.Id == id) is not Advert advert)
            return Results.BadRequest(new ErrorDto("Advert with given id is not found."));
        try
        {
            await imageService.RemoveImagesAsync(advert.Images);
        }
        catch { }
        db.Remove(advert);
        await db.SaveChangesAsync();
        return Results.Ok(new { });
    }

    private static async Task<IResult> Create(
        HttpContext context,
        IAdvertService advertService,
        ICategoryService categoryService,
        IImageService imageService,
        IMapper mapper,
        [FromForm] AdvertCreateRequest request)
    {
        if (categoryService.GetById(request.CategoryId) is not Category category)
            return Results.BadRequest(new ErrorDto("Category is not found."));
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var advert = mapper.Map<Advert>(request);
        advert.Images = await imageService.LoadImagesAsync(request.Images);
        advert.SellerId = userId;
        advertService.Create(advert);
        return Results.Ok(new { });
    }

    private static async Task<IResult> GetAll(
        HttpContext context,
        AdvertsDbContext db,
        IMapper mapper,
        IUserModule userModule,
        [FromQuery] int page = 1,
        [FromQuery(Name = "limit")] int pageSize = 9,
        [FromQuery] string? user = null)
    {
        var adverts = db.Adverts
            .Where(a => user == null || a.SellerId == user)
            .Include(a => a.Category)
            .ToList();
        var pageCount = (int)Math.Ceiling((double)adverts.Count / (double)pageSize);
        return Results.Ok(new
        {
            Adverts = adverts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(advert =>
            {
                var result = mapper.Map<AdvertDto>(advert);
                var seller = userModule.GetById(advert.SellerId);
                result.Contact.Name = seller.Name;
                result.Contact.Avatar = seller.Avatar;
                return result;
            }),
            PageCount = pageCount,
        });
    }
}