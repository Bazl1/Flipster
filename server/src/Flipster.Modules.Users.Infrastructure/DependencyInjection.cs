using Flipster.Modules.Users.Domain.Enums;
using Flipster.Modules.Users.Domain.Repositories;
using Flipster.Modules.Users.Domain.Services;
using Flipster.Modules.Users.Infrastructure.Auth;
using Flipster.Modules.Users.Infrastructure.Options;
using Flipster.Modules.Users.Infrastructure.Persistence;
using Flipster.Modules.Users.Infrastructure.Persistence.Repositories;
using Flipster.Modules.Users.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Flipster.Modules.Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDatabase(configuration)
            .AddAuth(configuration)

            .AddTransient<IAuthService, AuthService>()
            .AddTransient<IUserService, UserService>()
            
            .AddTransient<IPasswordHasher, PasswordHasher>()
            .AddTransient<ITokenGenerator, TokenGenerator>()

            .AddTransient<IUserRepository, UserRepository>()
            .AddTransient<ITokenRepository, TokenRepository>()
            .AddTransient<IFavoriteRepository, FavoriteRepository>()
            .AddTransient<ILocationRepository, LocationRepository>();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UsersModuleDbContext>(opt => 
            opt.UseInMemoryDatabase("Flipster.InMemoryDatabase"));
        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TokenOptions>(configuration.GetSection(nameof(TokenOptions)));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                var tokenOptions = configuration.GetSection(nameof(TokenOptions)).Get<TokenOptions>();
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecretKey))
                };
                opt.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Path.Value!.StartsWith("/hubs") &&
                            context.Request.Query.TryGetValue("access_token", out var accessToken))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            })
            .AddCookie(FlipsterAuthenticationSchemes.CookieScheme.SchemeName, opt => opt.Cookie.Name = FlipsterAuthenticationSchemes.CookieScheme.CookieName);
        services.AddAuthorization();
        return services;
    }
}