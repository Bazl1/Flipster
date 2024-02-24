using System.Security.Claims;
using Flipster.Modules.Users.Domain.Entities;
using Flipster.Modules.Users.Domain.Repositories;
using Flipster.Modules.Users.Dtos;
using Flipster.Shared.Domain.Errors;
using Flispter.Shared.Contracts.Catalog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;

namespace Flipster.Modules.Users.Endpoints.Favorites;

public static class FavoritesEndpoints
{
    public static IEndpointRouteBuilder MapFavoritesEndpoints(this IEndpointRouteBuilder builder,
        IConfiguration configuration)
    {
        builder.MapPost("/", (Delegate)Create);
        builder.MapDelete("/", (Delegate)Delete);
        builder.MapPost("/synchronizing", (Delegate)Synchronizing);
            
        return builder;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    private static async Task<IResult> Create(
        HttpContext context,
        [FromServices] IFavoriteRepository favoriteRepository,
        [FromServices] ICatalogModule catalogModule,
        [FromBody] Create.Request request)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (catalogModule.GetAdvertById(request.AdvertId) is null)
            throw new FlipsterError("Advert with given id is not found.");
        var favorite = new Favorite { UserId = userId, AdvertId = request.AdvertId };
        favoriteRepository.Add(favorite);
        return Results.Ok(new {});
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    private static async Task<IResult> Synchronizing(
        HttpContext context,
        [FromServices] IFavoriteRepository favoriteRepository,
        [FromServices] ICatalogModule catalogModule,
        [FromBody] Synchronizing.Request request)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        foreach (var id in request.Ids)
        {
            if (catalogModule.GetAdvertById(id) is not null)
            {
                var favorite = new Favorite { UserId = userId, AdvertId = id };
                favoriteRepository.Add(favorite);
            }
        }
        var favorites = favoriteRepository.GetByUserId(userId)
            .Select(f => new FavoriteDto { Id = f.AdvertId });
        return Results.Ok(favorites);
    }
}