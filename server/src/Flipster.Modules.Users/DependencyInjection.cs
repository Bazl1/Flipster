﻿using Flipster.Modules.Users.Contracts;
using Flipster.Modules.Users.Endpoints.Auth;
using Flipster.Modules.Users.Endpoints.Locations;
using Flipster.Modules.Users.Endpoints.Users;
using Flipster.Modules.Users.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Flispter.Shared.Contracts.Users;
using Flipster.Modules.Users.Endpoints.Favorites;

namespace Flipster.Modules.Users;

public static class DependencyInjection
{
    public static IEndpointRouteBuilder MapUsersModuleEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGroup("api/auth")
            .MapAuthEndpoints();
        builder.MapGroup("api/users")
            .MapUsersEndpoints();
        builder.MapGroup("api/locations")
            .MapLocationsEndpoints();
        builder.MapGroup("api/favorites")
            .MapFavoritesEndpoints();
        return builder;
    }

    public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddTransient<IUsersModule, UsersModule>()
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddInfrastructure(configuration);
        return services;
    }
}