using Flipster.Modules.Catalog.Endpoints.Adverts;
using Flipster.Modules.Catalog.Endpoints.Categories;
using Flipster.Modules.Catalog.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Flipster.Modules.Catalog;

public static class DependencyInjection
{
    public static IEndpointRouteBuilder MapCatalogModuleEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGroup("api/catalog")
            .MapAdvertsEndpoints();

        builder.MapGroup("api/categories")
            .MapCategoriesEndpoints();
        return builder;
    }

    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddInfrastructure(configuration);
        return services;
    }
}