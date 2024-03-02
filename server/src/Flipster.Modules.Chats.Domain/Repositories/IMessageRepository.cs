using Flipster.Modules.Chats.Domain.Entities;
using Flipster.Shared.Domain.Repositories;

namespace Flipster.Modules.Chats.Domain.Repositories;

public interface IMessageRepository : IRepository<Message>
{
    IEnumerable<Message> GetByChatId(string chatId);
}
