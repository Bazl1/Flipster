using System.Reflection;
using Flipster.Modules.Adverts.Data;
using Flipster.Modules.Adverts.Repositories;
using Flipster.Modules.Adverts.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flipster.Modules.Adverts;

public static class DependencyInjection
{
    public static IServiceCollection AddAdvertsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDatabase(configuration)
            
            .AddTransient<ICategoryService, CategoryService>()
            .AddTransient<ICategoryRepository, CategoryRepository>()
            
            .AddAutoMapper(Assembly.GetExecutingAssembly());
        
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<AdvertsDbContext>(options
                => options.UseInMemoryDatabase("Flipster.Adverts.Db"));
        
        return services;
    }
}