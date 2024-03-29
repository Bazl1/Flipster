﻿using AutoMapper;
using Flipster.Modules.Catalog.Domain.Entities;
using Flipster.Modules.Catalog.Domain.Enums;
using Flipster.Modules.Catalog.Domain.Repositories;
using Flipster.Modules.Catalog.Dtos;
using Flipster.Modules.Users.Domain.Enums;
using Flipster.Shared.Domain.Errors;
using Flipster.Shared.ImageStore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Flispter.Shared.Contracts.Users;

namespace Flipster.Modules.Catalog.Endpoints.Adverts;

internal static class AdvertsEndpoints
{
    public static IEndpointRouteBuilder MapAdvertsEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/", Create)
            .DisableAntiforgery();
        builder.MapPut("/{id}", Change)
            .DisableAntiforgery();
        builder.MapDelete("/{id}", Delete);
        builder.MapGet("/{id}", GetById);
        builder.MapGet("/", GetAll);
        return builder;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public static async Task<IResult> Create(
        HttpContext context,
        [FromServices] IAdvertRepository advertRepository,
        [FromServices] ICategoryRepository categoryRepository,
        [FromServices] IViewRepository viewRepository,
        [FromServices] IUsersModule usersModule,
        [FromServices] IImageService imageService,
        [FromServices] IMapper mapper,
        [FromForm] Create.Request request)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var advert = mapper.Map<Advert>(request);
        advert.SellerId = userId;
        if (!advert.IsFree && advert.Price == 0.00m)
            throw new FlipsterError("Unacceptable price. The price must be greater than zero.");
        if (categoryRepository.GetById(advert.CategoryId) is not Category category)
            throw new FlipsterError("Category invalid. Category with given id is not found.");
        advert.Images = (await imageService.LoadImagesAsync(request.Images)).ToList();
        advertRepository.Add(advert);
        var seller = usersModule.GetUserById(advert.SellerId);
        var result = new AdvertDto
        {
            Id = advert.Id,
            Title = advert.Title,
            Description = advert.Description,
            Images = advert.Images,
            IsFree = advert.IsFree,
            Price = advert.Price != null ? advert.Price.ToString() : string.Empty,
            BusinessType = advert.BusinessType.ToString(),
            ProductType = advert.ProductType.ToString(),
            Status = advert.Status.ToString(),
            CreatedAt = advert.CreatedAt.ToString(),
            Category = mapper.Map<CategoryDto>(category),
            Contact = new ContactDto { Id = advert.SellerId, Name = seller.Name, Avatar = seller.Avatar, Email = advert.Email, Location = advert.Location, PhoneNumber = advert.PhoneNumber },
            Views = viewRepository.GetCountByAdvertId(advert.Id)
        };
        return Results.Ok(result);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public static async Task<IResult> Change(
        HttpContext context,
        [FromServices] IAdvertRepository advertRepository,
        [FromServices] ICategoryRepository categoryRepository,
        [FromServices] IViewRepository viewRepository,
        [FromServices] IUsersModule usersModule,
        [FromServices] IImageService imageService,
        [FromServices] ILogger<ImageService> _imageServiceLogger,
        [FromServices] IMapper mapper,
        [FromRoute] string id,
        [FromForm] Change.Request request)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var role = context.User.FindFirstValue(ClaimTypes.Role);
        if (advertRepository.GetById(id) is not Advert advert)
            throw new FlipsterError("Advert with given id is not found.");
        if (advert.SellerId != userId && role != UserRole.Admin.ToString())
            throw new FlipsterError("This user is denied access.");
        if (!advert.IsFree && advert.Price == 0.00m)
            throw new FlipsterError("Unacceptable price. The price must be greater than zero.");
        advert.Title = request.Title;
        advert.Description = request.Description;
        if (categoryRepository.GetById(request.CategoryId) is not Category category)
            throw new FlipsterError("Category with given id is not found.");
        advert.CategoryId = category.Id;
        {
            if (request.ImageUrls == null)
            {
                try
                {
                    await imageService.RemoveImagesAsync(advert.Images);
                    advert.Images.RemoveRange(0, advert.Images.Count);
                }
                catch
                {
                    _imageServiceLogger.LogError("Error with deleting images.");
                }
            }
            var removeUrls = new List<string>();
            foreach (var url in advert.Images)
            {
                if (!request.ImageUrls.Contains(url))
                {
                    try
                    {
                        await imageService.RemoveImageAsync(url);
                        removeUrls.Add(url);
                    }
                    catch
                    {
                        _imageServiceLogger.LogError($"Url: {url} was not deleted due to server problems.");
                    }
                }
            }
            foreach (var url in removeUrls)
                advert.Images.Remove(url);
            if (request.Images != null)
                advert.Images.AddRange(await imageService.LoadImagesAsync(request.Images));
        }
        advert.IsFree = request.IsFree;
        advert.Price = request.Price;
        advert.ProductType = Enum.Parse<ProductType>(request.ProductType);
        advert.BusinessType = Enum.Parse<BusinessType>(request.BusinessType);
        advert.Location = request.Location;
        advert.Email = request.Email;
        advert.PhoneNumber = request.PhoneNumber;
        advertRepository.Update(advert);
        var seller = usersModule.GetUserById(advert.SellerId);
        var result = new AdvertDto
        {
            Id = advert.Id,
            Title = advert.Title,
            Description = advert.Description,
            Images = advert.Images,
            IsFree = advert.IsFree,
            Price = advert.Price != null ? advert.Price.ToString() : string.Empty,
            BusinessType = advert.BusinessType.ToString(),
            ProductType = advert.ProductType.ToString(),
            Status = advert.Status.ToString(),
            CreatedAt = advert.CreatedAt.ToString(),
            Category = mapper.Map<CategoryDto>(category),
            Contact = new ContactDto { Id = advert.SellerId, Name = seller.Name, Avatar = seller.Avatar, Email = advert.Email, Location = advert.Location, PhoneNumber = advert.PhoneNumber },
            Views = viewRepository.GetCountByAdvertId(advert.Id)
        };
        return Results.Ok(result);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public static async Task<IResult> Delete(
        HttpContext context,
        [FromServices] IAdvertRepository advertRepository,
        [FromServices] IViewRepository viewRepository,
        [FromRoute] string id)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var role = context.User.FindFirstValue(ClaimTypes.Role);
        if (advertRepository.GetById(id) is not Advert advert)
            throw new FlipsterError("Advert with given id is not found.");
        if (advert.SellerId != userId && role != UserRole.Admin.ToString())
            throw new FlipsterError("This user is denied access.");
        advertRepository.Remove(advert);
        return Results.Ok(new { });
    }

    [Authorize(AuthenticationSchemes = $"Flipster.Cookies.Visitor,{JwtBearerDefaults.AuthenticationScheme}")]
    private static async Task<IResult> GetById(
        HttpContext context,
        [FromServices] IAdvertRepository advertRepository,
        [FromServices] ICategoryRepository categoryRepository,
        [FromServices] IViewRepository viewRepository,
        [FromServices] IUsersModule usersModule,
        [FromServices] IMapper mapper,
        [FromRoute] string id)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (advertRepository.GetById(id) is not Advert advert)
            throw new FlipsterError("Advert with given id is not found.");
        viewRepository.Add(new View { AdvertId = advert.Id, UserId = userId });
        var seller = usersModule.GetUserById(advert.SellerId);
        var result = new AdvertDto
        {
            Id = advert.Id,
            Title = advert.Title,
            Description = advert.Description,
            Images = advert.Images,
            IsFree = advert.IsFree,
            Price = advert.Price != null ? advert.Price.ToString() : string.Empty,
            BusinessType = advert.BusinessType.ToString(),
            ProductType = advert.ProductType.ToString(),
            Status = advert.Status.ToString(),
            CreatedAt = advert.CreatedAt.ToString(),
            Category = mapper.Map<CategoryDto>(advert.Category),
            Contact = new ContactDto { Id = advert.SellerId, Name = seller.Name, Avatar = seller.Avatar, Email = advert.Email, Location = advert.Location, PhoneNumber = advert.PhoneNumber },
            Views = viewRepository.GetCountByAdvertId(advert.Id)
        };
        return Results.Ok(result);
    }

    public static async Task<IResult> GetAll(
        HttpContext context,
        [FromServices] IAdvertRepository advertRepository,
        [FromServices] IViewRepository viewRepository,
        [FromServices] IUsersModule usersModule,
        [FromServices] IMapper mapper,
        [FromQuery] int page,
        [FromQuery] int limit,
        [FromQuery] string? userId = "",
        [FromQuery] string? query = "",
        [FromQuery] int min = -1,
        [FromQuery] int max = -1,
        [FromQuery] string categoryId = "",
        [FromQuery] string location = "",
        [FromQuery] string sort = "")
    {
        var result = new GetAll.Response();
        List<Advert> items;
        if (userId != "")
            items = advertRepository.GetByUserId(userId).ToList();
        else
            items = advertRepository.Search(query: query, min: min, max: max, categoryId: categoryId, location: location).ToList();
        result.PageCount = (int)Math.Ceiling((double)items.Count / (double)limit);
        items = sort switch
        {
            "PopularFirst" => items.OrderByDescending(a => viewRepository.GetCountByAdvertId(a.Id)).ToList(),
            "CheapOnesFirst" => items.OrderBy(i => i.Price.Value).ToList(),
            "DearOnesFirst" => items.OrderByDescending(i => i.Price.Value).ToList(),
            _ => items
        };
        result.Adverts = items
            .Skip((page - 1) * limit)
            .Take(limit)
            .Select(advert => 
            {
                var seller = usersModule.GetUserById(advert.SellerId);
                return new AdvertDto
                {
                    Id = advert.Id,
                    Title = advert.Title,
                    Description = advert.Description,
                    Images = advert.Images,
                    IsFree = advert.IsFree,
                    Price = advert.Price != null ? advert.Price.ToString() : string.Empty,
                    BusinessType = advert.BusinessType.ToString(),
                    ProductType = advert.ProductType.ToString(),
                    Status = advert.Status.ToString(),
                    CreatedAt = advert.CreatedAt.ToString(),
                    Category = mapper.Map<CategoryDto>(advert.Category),
                    Contact = new ContactDto { Id = advert.SellerId, Name = seller.Name, Avatar = seller.Avatar, Email = advert.Email, Location = advert.Location, PhoneNumber = advert.PhoneNumber },
                    Views = viewRepository.GetCountByAdvertId(advert.Id)
                };
            });
        return Results.Ok(result);
    }
}
