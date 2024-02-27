using System.Security.Claims;
using Flipster.Modules.Users.Domain.Entities;
using Flipster.Modules.Users.Domain.Repositories;
using Flipster.Modules.Users.Dtos;
using Flipster.Modules.Users.Infrastructure.Auth;
using Flipster.Shared.Contracts.Catalog.Dtos;
using Flipster.Shared.Domain.Errors;
using Flispter.Shared.Contracts.Catalog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Flipster.Modules.Users.Endpoints.Favorites;

public static class FavoritesEndpoints
{
    public static IEndpointRouteBuilder MapFavoritesEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/", (Delegate)Create);
        // builder.MapDelete("/", (Delegate)Delete);
        builder.MapGet("/ids", (Delegate)GetAllIds);
        builder.MapGet("/", (Delegate)GetAll);

        return builder;
    }

    [Authorize(AuthenticationSchemes = $"{FlipsterAuthenticationSchemes.CookieScheme.SchemeName},{JwtBearerDefaults.AuthenticationScheme}")]
    private static async Task<IResult> Create(
        HttpContext context,
        [FromServices] IFavoriteRepository favoriteRepository,
        [FromServices] ICatalogModule catalogModule,
        [FromBody] Create.Request request)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (favoriteRepository.GetById(userId, request.AdvertId) is Favorite favorite)
        {
            favoriteRepository.Remove(favorite);
        }
        else
        { 
            var favorite1 = new Favorite { UserId = userId, AdvertId = request.AdvertId };
            favoriteRepository.Add(favorite1);
        }
        return Results.Ok(request);
    }

    [Authorize(AuthenticationSchemes = $"{FlipsterAuthenticationSchemes.CookieScheme.SchemeName},{JwtBearerDefaults.AuthenticationScheme}")]
    private static async Task<IResult> Delete(
        HttpContext context,
        [FromServices] IFavoriteRepository favoriteRepository,
        [FromBody] Delete.Request request)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (favoriteRepository.GetByUserIdAndAdvertId(userId, request.AdvertId) is not Favorite favorite)
            throw new FlipsterError("Favorite is not found.");
        favoriteRepository.Remove(favorite);
        return Results.Ok(new {});
    }

    [Authorize(AuthenticationSchemes = $"{FlipsterAuthenticationSchemes.CookieScheme.SchemeName},{JwtBearerDefaults.AuthenticationScheme}")]
    private static async Task<IResult> GetAllIds(
        HttpContext context,
        [FromServices] IFavoriteRepository favoriteRepository,
        [FromServices] ICatalogModule catalogModule)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var favorites = favoriteRepository.GetByUserId(userId)
            .Select(f => new FavoriteDto { Id = f.AdvertId });
        return Results.Ok(favorites);
    }

    [Authorize(AuthenticationSchemes = $"{FlipsterAuthenticationSchemes.CookieScheme.SchemeName},{JwtBearerDefaults.AuthenticationScheme}")]
    private static async Task<IResult> GetAll(
        HttpContext context,
        [FromServices] IFavoriteRepository favoriteRepository,
        [FromServices] IUserRepository userRepository,
        [FromServices] ICatalogModule catalogModule,
        [FromQuery] int page,
        [FromQuery] int limit)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var response = new GetAll.Response();
        var adverts = favoriteRepository
            .GetByUserId(userId)
            .Join(catalogModule.GetAll(), f => f.AdvertId, a => a.Id, (f, a) => a);
        response.PageCount = (int)Math.Ceiling((float)adverts.Count() / (float)limit);
        response.Adverts = adverts
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToList();
        return Results.Ok(response);
    }
}