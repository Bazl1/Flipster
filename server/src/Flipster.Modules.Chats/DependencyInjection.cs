using Flipster.Modules.Chats.Endpoints.Chats;
using Flipster.Modules.Chats.Endpoints.Messages;
using Flipster.Modules.Chats.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flipster.Modules.Chats;

public static class DependencyInjection
{
    public static IEndpointRouteBuilder MapChatsModuleEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGroup("api/chats/")
            .MapChatsEndpoints()
            .MapGroup("{chatId}/messages")
            .MapMessagesEndpoints();

        builder.MapHub<ChatsHub>("hubs/chats");

        return builder;
    }

    public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSignalR();
        return services;
    }
}