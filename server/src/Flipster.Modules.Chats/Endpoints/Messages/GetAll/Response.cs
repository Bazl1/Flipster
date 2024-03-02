using Flipster.Modules.Chats.Dtos;

namespace Flipster.Modules.Chats.Endpoints.Messages.GetAll;

internal record Response(
    IEnumerable<MessageDto> Messages);
