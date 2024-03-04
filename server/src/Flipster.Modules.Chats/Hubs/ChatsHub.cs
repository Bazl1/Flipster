using Flipster.Modules.Chats.Domain.Entities;
using Flipster.Modules.Chats.Domain.Repositories;
using Flipster.Modules.Chats.Dtos;
using Flispter.Shared.Contracts.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Flipster.Modules.Chats.Hubs;

internal class UserSession
{
    public string ConnectionId { get; set; }
    public string ChatId { get; set; }

    public UserSession(string connectionId, string chatId)
    {
        ConnectionId = connectionId;
        ChatId = chatId;
    }
}

public class ChatsHub(
    IMessageRepository _messageRepository,
    IChatRepository _chatRepository,
    IUsersModule _usersModule) : Hub
{
    private const string NewMessageEvent = "e:messages:new";
    private const string RemoveMessageEvent = "e:messages:removed";
    private const string ReviewedMessageEvent = "e:messages:reviewed";
    private const string ChangedMessageEvent = "e:messages:changed";
    private const string ErrorEvent = "e:error";
    private const string SuccessEvent = "e:success";
    private static Dictionary<string, UserSession> _users = new();
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task StartReceivingMessages(string chatId)
    {
        var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (_chatRepository.GetById(chatId) is not Chat chat)
        {
            await Clients.Caller.SendAsync(ErrorEvent, "Chat with given id is not found.");
            return;
        }
        if (!chat.IsMember(userId))
        {
            await Clients.Caller.SendAsync(ErrorEvent, "You're not a member of the chat room.");
            return;
        }
        _users.Add(userId, new UserSession(Context.ConnectionId, chatId));
        await Clients.Caller.SendAsync(SuccessEvent);
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        if (_users.ContainsKey(chat.GetInterlocutorByMemberId(userId)) &&
            _users[chat.GetInterlocutorByMemberId(userId)].ChatId == chatId)
            await Clients
                .Client(_users[chat.GetInterlocutorByMemberId(userId)].ConnectionId)
                .SendAsync(ReviewedMessageEvent);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task RemoveMessage(string messageId)
    {
        var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (_messageRepository.GetById(messageId) is not Message message)
        {
            await Clients
                .Caller
                .SendAsync(ErrorEvent, "Message with given id is not found.");
            return;
        }
        if (message.FromId != userId)
        {
            await Clients
                .Caller
                .SendAsync(ErrorEvent, "You cannot delete a conversation partner's message.");
            return;
        }
        message.Text = "This message has been deleted.";
        message.IsDeleted = true;
        _messageRepository.Update(message);
        if (_users.ContainsKey(message.ToId))
            await Clients
                .Client(_users[message.ToId].ConnectionId)
                .SendAsync(RemoveMessageEvent, message.Id);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task ChangeMessage(string messageId, string text)
    {
        var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (_messageRepository.GetById(messageId) is not Message message)
        {
            await Clients
                .Caller
                .SendAsync(ErrorEvent, "Message with given id is not found.");
            return;
        }
        if (message.FromId != userId)
        {
            await Clients
                .Caller
                .SendAsync(ErrorEvent, "You cannot delete a conversation partner's message.");
            return;
        }
        message.Text = text;
        _messageRepository.Update(message);
        if (_users.ContainsKey(message.ToId))
            await Clients
                .Client(_users[message.ToId].ConnectionId)
                .SendAsync(ChangedMessageEvent, message.Id);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task SendMessage(string chatId, string text)
    {
        var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (_chatRepository.GetById(chatId) is not Chat chat)
        { 
            await Clients.Caller.SendAsync(ErrorEvent, "Chat with given id is not found.");
            return;
        }
        var interlocutorOnline = _users.ContainsKey(chat.GetInterlocutorByMemberId(userId));
        var message = new Message
        {
            ChatId = chat.Id,
            FromId = userId,
            ToId = chat.GetInterlocutorByMemberId(userId),
            Text = text,
            IsRead = interlocutorOnline
        };
        _messageRepository.Add(message);

        var user = _usersModule.GetUserById(userId);
        var messageResult = new MessageDto
        {
            Id = message.Id,
            From = new UserDto { Id = user.Id, Name = user.Name, Avatar = user.Avatar },
            Text = message.Text,
            IsRead = message.IsRead,
            CreatedAt = message.CreatedAt.ToString("dd.MM.yyyy H:mm"),
            IsDeleted = message.IsDeleted,
        };

        if (!interlocutorOnline)
            await Clients
                .Caller
                .SendAsync(NewMessageEvent, messageResult);
        else
            await Clients
                .Group(chatId)
                .SendAsync(NewMessageEvent, messageResult);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async void EndReceivingMessages(string chatId)
    {
        var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
        _users.Remove(userId);
    }
}