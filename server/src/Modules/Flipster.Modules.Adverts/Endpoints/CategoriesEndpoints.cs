using AutoMapper;
using Flipster.Modules.Adverts.Dto;
using Flipster.Modules.Adverts.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Flipster.Modules.Adverts.Endpoints;

public static class CategoriesEndpoints
{
    public static IEndpointRouteBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/", GetAll);
        
        return builder;
    }

    private static async Task<IResult> GetAll(
        HttpContext context,
        [FromServices] ICategoryService categoryService,
        [FromServices] IMapper mapper)
    {
        return Results.Ok(mapper.Map<List<CategoryDto>>(categoryService.GetAll()));
    }
}