using Flipster.Modules.Chats.Domain.Entities;
using Flipster.Modules.Chats.Domain.Repositories;
using Flipster.Modules.Chats.Dtos;
using Flispter.Shared.Contracts.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Text.Json;

namespace Flipster.Modules.Chats.Hubs;

public class ChatsHub(
    IMessageRepository _messageRepository,
    IChatRepository _chatRepository,
    IUsersModule _usersModule) : Hub
{
    private const string NewMessageEvent = "e:messages:new";
    private const string RemoveMessageEvent = "e:messages:removed";
    private const string ErrorEvent = "e:error";

    public Dictionary<string, Dictionary<string, string>> Chats = new();
    public Dictionary<string, string> Users = new();

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task StartReceivingMessages(string chatId)
    {
        var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        Dictionary<string, string>? members;
        if (!Chats.TryGetValue(chatId, out members))
        {
            members = new();
            Chats.Add(chatId, members);
        }
        members.Add(userId, Context.ConnectionId);
        Users.Add(userId, Context.ConnectionId);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task RemoveMessage(string messageId)
    {
        var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (_messageRepository.GetById(messageId) is not Message message)
        {
            await Clients.Caller.SendAsync(ErrorEvent, "Message with given id is not found.");
            return;
        }
        if (message.FromId != userId)
        {
            await Clients.Caller.SendAsync(ErrorEvent, "You cannot delete a conversation partner's message.");
            return;
        }
        message.Text = "This message has been deleted.";
        _messageRepository.Update(message);
        if (Chats[message.ChatId].TryGetValue(message.ToId, out string? toConnectionId))
        {
            await Clients.Client(toConnectionId).SendAsync(RemoveMessageEvent, message.Id);
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task SentMessage(string chatId, string to, string text)
    {
        var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (_chatRepository.GetById(chatId) is not Chat chat)
        { 
            await Clients.Caller.SendAsync(ErrorEvent, "Chat with given id is not found.");
            return;
        }
        var interlocutorOnline = Chats[chatId].ContainsKey(to);
        var message = new Message
        {
            ChatId = chat.Id,
            FromId = userId,
            ToId = chat.GetInterlocutorByMemberId(userId),
            Text = text,
            IsRead = interlocutorOnline
        };
        _messageRepository.Add(message);

        if (!interlocutorOnline)
            return;

        var user = _usersModule.GetUserById(userId);
        var messageResult = JsonSerializer.Serialize(
            new MessageDto
            {
                Id = message.Id,
                From = new UserDto { Id = user.Id, Name = user.Name, Avatar = user.Avatar },
                Text = message.Text,
                IsRead = message.IsRead,
                CreatedAt = message.CreatedAt.ToString("dd.MM.yyyy H:mm"),
            }, 
            options: new JsonSerializerOptions
            { 
                PropertyNameCaseInsensitive = true 
            });
        await Clients.Client(Chats[chatId][to]).SendAsync(RemoveMessageEvent, messageResult);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public void EndReceivingMessages(string chatId)
    {
        var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        Dictionary<string, string>? members;
        if (!Chats.TryGetValue(chatId, out members))
            return;
        if (!members.TryGetValue(userId, out string? connectionId))
            return;
        members.Remove(userId);
    }
}