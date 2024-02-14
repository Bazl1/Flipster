using AutoMapper;
using Flipster.Modules.Locations.Dto;
using Flipster.Modules.Locations.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Flipster.Modules.Images.Endpoints;

public static class LocationsEndpoints
{
    public static IEndpointRouteBuilder MapLocationsEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/", GetAll);
        
        return builder;
    }

    private static async Task<IResult> GetAll(
        HttpContext context,
        [FromServices] ILocationService locationService,
        [FromServices] IMapper mapper)
    {
        return Results.Ok(mapper.Map<LocationDto[]>(locationService.GetAll()));
    }
}