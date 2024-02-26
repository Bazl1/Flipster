using Flipster.Modules.Catalog.Endpoints.Adverts;
using Flipster.Modules.Catalog.Endpoints.Categories;
using Flipster.Modules.Catalog.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Flipster.Modules.Catalog.Contracts;
using Flispter.Shared.Contracts.Catalog;
using Flipster.Modules.Catalog.Endpoints.Recommendations;

namespace Flipster.Modules.Catalog;

public static class DependencyInjection
{
    public static IEndpointRouteBuilder MapCatalogModuleEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGroup("api/catalog")
            .MapAdvertsEndpoints();
        
        builder.MapGroup("api/catalog/recommendations")
            .MapRecommendationsEndpoints();

        builder.MapGroup("api/categories")
            .MapCategoriesEndpoints();

        return builder;
    }

    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddTransient<ICatalogModule, CatalogModule>()
            .AddInfrastructure(configuration);
        return services;
    }
}