using System.Reflection;
using Flipster.Modules.Locations.Contracts;
using Flipster.Modules.Locations.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flipster.Modules.Locations;

public static class DependencyInjection
{
    public static IServiceCollection AddLocationsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddTransient<ILocationService, LocationService>()
            .AddTransient<ILocationModule, LocationModule>()
        
            .AddAutoMapper(Assembly.GetExecutingAssembly());
        
        return services;
    }
}