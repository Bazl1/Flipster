using Flipster.Modules.Chats.Domain.Entities;
using Flipster.Modules.Chats.Domain.Repositories;
using Flipster.Modules.Chats.Dtos;
using Flipster.Shared.Domain.Errors;
using Flispter.Shared.Contracts.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Flipster.Modules.Chats.Endpoints.Messages;

internal static class MessagesEndpoints
{
    public static IEndpointRouteBuilder MapMessagesEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/", (Delegate)GetAll);
        return builder;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    private static async Task<IResult> GetAll(
        HttpContext context,
        [FromServices] IMessageRepository messageRepository,
        [FromServices] IChatRepository chatRepository,
        [FromServices] IUsersModule usersModule,
        [FromRoute] string chatId)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (chatRepository.GetById(chatId) is not Chat chat)
            throw new FlipsterError("Chat with given id is not found.");

        foreach (var message in chat.Messages)
            if (message.ToId == userId)
                message.IsRead = true;
        chatRepository.Update(chat);

        var messages = chat.Messages.Select(m => 
        {
            var from = usersModule.GetUserById(m.FromId);
            return new MessageDto
            {
                Id = m.Id,
                From = new UserDto { Id = from.Id, Name = from.Name, Avatar = from.Avatar },
                Text = m.Text,
                IsRead = m.IsRead,
                CreatedAt = m.CreatedAt.ToString("dd.MM.yyyy H:mm"),
                IsDeleted = m.IsDeleted,
            };
        });
        return Results.Ok(new GetAll.Response(messages));
    }
}
