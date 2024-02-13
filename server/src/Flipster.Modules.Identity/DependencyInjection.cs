using System.Reflection;
using Flipster.Modules.Identity.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flipster.Modules.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddInfrastructure(configuration)
            
            .AddAutoMapper(Assembly.GetExecutingAssembly());
        
        return services;
    }
}