using AutoMapper;
using Flipster.Modules.Catalog.Domain.Entities;
using Flipster.Modules.Catalog.Domain.Repositories;
using Flipster.Modules.Catalog.Dtos;
using Flipster.Shared.Domain.Errors;
using Flispter.Shared.Contracts.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Flipster.Modules.Catalog.Endpoints.Recommendations;

internal static class RecommendationsEndpoints
{
    public static IEndpointRouteBuilder MapRecommendationsEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/", (Delegate)GetRecommendations);
        builder.MapGet("/{id}", (Delegate)GetRecommendationsByAdvertId);

        return builder;
    }

    private static async Task<IResult> GetRecommendations(
        HttpContext context,
        [FromServices] IMapper mapper,
        [FromServices] IAdvertRepository advertRepository,
        [FromServices] IViewRepository viewRepository,
        [FromServices] IUsersModule usersModule,
        [FromQuery] int page,
        [FromQuery] int limit)
    {
        string? userId = null;
        try { userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier); } catch { };

        var response = new GetRecommendations.Response();
        var adverts = advertRepository.GetAll()
            .Where(a => userId == null ||
                a.SellerId != userId ||
                usersModule.GetFavoritesByUserId(userId).Count() == 0 ||
                usersModule.GetFavoritesByUserId(userId)
                    .Select(f => f.AdvertId)
                    .Join(advertRepository.GetAll(), id => id, a => a.Id, (id, a) => a)
                    .Select(a => a.CategoryId)
                    .Contains(a.CategoryId)
            )
            .OrderByDescending(a => viewRepository.GetCountByAdvertId(a.Id))
            .ThenBy(a => a.CreatedAt);
        response.PageCount = (int)Math.Ceiling((float)adverts.Count() / (float)limit);
        response.Adverts = adverts
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
                    Contact = new ContactDto { Id = advert.Id, Name = seller.Name, Avatar = seller.Avatar, Email = advert.Email, Location = advert.Location, PhoneNumber = advert.PhoneNumber },
                    Views = viewRepository.GetCountByAdvertId(advert.Id)
                };
            })
            .ToList();

        return Results.Ok(response);
    }

    private static async Task<IResult> GetRecommendationsByAdvertId(
        HttpContext context,
        [FromServices] IAdvertRepository advertRepository,
        [FromServices] IViewRepository viewRepository,
        [FromServices] IUsersModule usersModule,
        [FromServices] IMapper mapper,
        [FromRoute] string id,
        [FromQuery] int page,
        [FromQuery] int limit = 4)
    {
        string? userId = null;
        try { userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier); } catch { };

        if (advertRepository.GetById(id) is not Advert advert)
            throw new FlipsterError("Advert with given id is not found.");

        var response = new GetRecommendations.Response();
        var adverts = advertRepository
            .GetByCategoryId(advert.CategoryId)
            .Where(a => (userId == null || a.SellerId != userId) && (a.Id != id))
            .OrderByDescending(a => advert.Title.ToUpper().Split(' ').Any(kw => a.Title.ToUpper().Contains(kw) || a.Description.ToUpper().Contains(kw)))
            .ThenByDescending(a => viewRepository.GetCountByAdvertId(a.Id))
            .ThenBy(a => a.CreatedAt);
        response.PageCount = 0;// (int)Math.Ceiling((float)adverts.Count() / (float)limit);
        response.Adverts = adverts
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
                    Contact = new ContactDto { Id = advert.Id, Name = seller.Name, Avatar = seller.Avatar, Email = advert.Email, Location = advert.Location, PhoneNumber = advert.PhoneNumber },
                    Views = viewRepository.GetCountByAdvertId(advert.Id)
                };
            })
            .ToList();

        return Results.Ok(response);
    }
}
