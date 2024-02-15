using System.Security.Claims;
using AutoMapper;
using Flipster.Modules.Adverts.Data;
using Flipster.Modules.Adverts.Dto;
using Flipster.Modules.Adverts.Entities;
using Flipster.Modules.Adverts.Services;
using Flipster.Modules.Adverts.ValueObjects;
using Flipster.Modules.Images.Contracts;
using Flipster.Modules.Images.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Flipster.Modules.Adverts.Endpoints;

public static class AdvertsEndpoints
{
    public static IEndpointRouteBuilder MapAdvertsEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/", Create)
            .RequireAuthorization()
            .DisableAntiforgery();
        
        builder.MapGet("/", GetAll);
        
        return builder;
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
        advert.Contact = new AdvertContact(userId, request.Location, request.Email, request.PhoneNumber);
        advertService.Create(advert);
        return Results.Created();
    }
    
    private static async Task<IResult> GetAll(
        HttpContext context,
        AdvertsDbContext db,
        [FromQuery] int page = 1,
        [FromQuery(Name = "limit")] int pageSize = 9,
        [FromQuery] string? user = null)
    {
        var adverts = db.Adverts
            .Where(a =>
                (user == null || a.Contact.SellerId == user))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        var pageCount = (int)Math.Ceiling((double)adverts.Count / (double)pageSize);
        return Results.Ok(new
        {
            Adverts = adverts, 
            PageCount = pageCount,
        });
    }    
}