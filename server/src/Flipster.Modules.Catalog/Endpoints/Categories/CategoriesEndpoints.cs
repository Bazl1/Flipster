using AutoMapper;
using Flipster.Modules.Catalog.Domain.Repositories;
using Flipster.Modules.Catalog.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Flipster.Modules.Catalog.Endpoints.Categories;

internal static class CategoriesEndpoints
{
    public static IEndpointRouteBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/", GetAll);

        return builder;
    }

    private static async Task<IResult> GetAll(
        HttpContext context,
        [FromServices] ICategoryRepository categoryRepository,
        [FromServices] IMapper mapper)
    {
        return Results.Ok(mapper.Map<IEnumerable<CategoryDto>>(categoryRepository.GetAll()));
    }
}
