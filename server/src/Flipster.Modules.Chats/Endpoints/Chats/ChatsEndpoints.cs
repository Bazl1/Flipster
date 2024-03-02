using AutoMapper.Execution;
using Flipster.Modules.Chats.Domain.Entities;
using Flipster.Modules.Chats.Domain.Repositories;
using Flipster.Modules.Chats.Dtos;
using Flipster.Shared.Domain.Errors;
using Flispter.Shared.Contracts.Catalog;
using Flispter.Shared.Contracts.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Flipster.Modules.Chats.Endpoints.Chats;

internal static class ChatsEndpoints
{
    public static IEndpointRouteBuilder MapChatsEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/", (Delegate)Create);
        builder.MapGet("/", (Delegate)GetAll);
        builder.MapDelete("/{chatId}", (Delegate)Delete);
        return builder;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    private static async Task<IResult> Create(
        HttpContext context,
        [FromServices] IChatRepository chatRepository,
        [FromServices] ICatalogModule catalogModule,
        [FromBody] Create.Request request)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var advert = catalogModule.GetAdvertById(request.AdvertId);
        var chat = new Chat
        { 
            Title = advert.Title,
            FirstMemberId = userId,
            SecondMemberId = advert.Contact.Id,
        };
        chatRepository.Add(chat);
        return Results.Ok(new Create.Response(chat.Id));
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    private static async Task<IResult> GetAll(
        HttpContext context,
        [FromServices] IChatRepository chatRepository,
        [FromServices] IUsersModule usersModule)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var chats = chatRepository.GetByMemberId(userId)
            .Select(c =>
            {
                var interlocutor = usersModule.GetUserById(userId == c.FirstMemberId ? c.SecondMemberId : c.FirstMemberId);
                return new ChatDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Interlocutor = new UserDto { Id = interlocutor.Id, Name = interlocutor.Name, Avatar = interlocutor.Avatar },
                    UnreadMessageCount = c.GetUnredMessagesCountByMemberId(userId)
                };
            });
        return Results.Ok(new GetAll.Response(chats));
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    private static async Task<IResult> Delete(
        HttpContext context,
        [FromServices] IChatRepository chatRepository,
        [FromRoute] string chatId)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (chatRepository.GetById(chatId) is not Chat chat)
            throw new FlipsterError("Chat with given id is not found.");
        if (chat.FirstMemberId != userId && chat.SecondMemberId != userId)
            throw new FlipsterError("The user is not a member of the chat room.");
        chatRepository.Remove(chat);
        return Results.Ok();
    }
}