using Flipster.Modules.Chats.Dtos;

namespace Flipster.Modules.Chats.Endpoints.Chats.GetAll;

internal record Response(
    IEnumerable<ChatDto> Chats);
