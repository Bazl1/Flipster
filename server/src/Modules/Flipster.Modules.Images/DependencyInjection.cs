using Flipster.Modules.Images.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flipster.Modules.Images;

public static class DependencyInjection
{
    public static IServiceCollection AddImagesModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IImageService, ImageService>();
        return services;
    }
}