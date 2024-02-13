using System.Text;
using Flipster.Modules.Identity.Domain.Infrastructure.Persistance;
using Flipster.Modules.Identity.Domain.Infrastructure.User;
using Flipster.Modules.Identity.Domain.User.Repositories;
using Flipster.Modules.Identity.Domain.User.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Flipster.Modules.Identity.Domain.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDatabase(configuration)
            .AddAuth(configuration)
            
            .AddTransient<IPasswordHasher, PasswordHasher>()
            .AddTransient<ITokenGenerator, TokenGenerator>()
            
            .AddTransient<IUserRepository, UserRepository>()
            .AddTransient<ITokenRepository, TokenRepository>();

        return services;
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(options
            => options.UseInMemoryDatabase("Flipster.Identity.DB"));
        return services;
    }
    
    private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TokenOptions>(configuration.GetSection(nameof(TokenOptions)));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var tokenOptions = configuration.GetSection(nameof(TokenOptions)).Get<TokenOptions>();
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecretKey))
                };
            });
        services.AddAuthorization();
        return services;
    }
}