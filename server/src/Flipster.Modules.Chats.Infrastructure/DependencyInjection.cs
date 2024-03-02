using Flipster.Modules.Chats.Domain.Repositories;
using Flipster.Modules.Chats.Infrastructure.Persistence;
using Flipster.Modules.Chats.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flipster.Modules.Chats.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddTransient<IChatRepository, ChatRepository>()
            .AddTransient<IMessageRepository, MessageRepository>();
        services.AddDbContext<ChatsModuleDbContext>(opt =>
            opt.UseInMemoryDatabase("Flipster.InMemoryDatabase"));
        return services;
    }
}