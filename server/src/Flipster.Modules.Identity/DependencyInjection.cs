using System.Reflection;
using Flipster.Modules.Identity.Domain.Infrastructure;
using Flipster.Modules.Identity.Domain.User.Entities.Contracts;
using Flipster.Modules.Identity.Domain.User.Entities.Contracts.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flipster.Modules.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddInfrastructure(configuration)

            .AddTransient<IUserModule, UserModule>()
            .AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}