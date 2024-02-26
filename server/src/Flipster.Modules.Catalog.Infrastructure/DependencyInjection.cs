using Flipster.Modules.Catalog.Domain.Repositories;
using Flipster.Modules.Catalog.Infrastructure.Persistence;
using Flipster.Modules.Catalog.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flipster.Modules.Catalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogModuleContext>(opt =>
            opt.UseInMemoryDatabase("Flipster.InMemoryDatabase"));
        services
            .AddTransient<IAdvertRepository, AdvertRepository>()
            .AddTransient<IViewRepository, ViewRepository>()
            .AddTransient<ICategoryRepository, CategoryRepository>();
        return services;
    }
}