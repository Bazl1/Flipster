using AutoMapper;
using Flipster.Modules.Users.Domain.Repositories;
using Flipster.Modules.Users.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Flipster.Modules.Users.Endpoints.Locations;

public static class LocationsEndpoints
{
    public static IEndpointRouteBuilder MapLocationsEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/", (Delegate)GetAll);
        return builder;
    }

    private static async Task<IResult> GetAll(
        HttpContext context,
        [FromServices] ILocationRepository locationRepository,
        [FromServices] IMapper mapper)
    {
        return Results.Ok(mapper.Map<IEnumerable<LocationDto>>(locationRepository.GetAll()));
    }
}
