using Flipster.Modules.Chats.Domain.Entities;
using Flipster.Shared.Domain.Repositories;

namespace Flipster.Modules.Chats.Domain.Repositories;

public interface IChatRepository : IRepository<Chat>
{
    IEnumerable<Chat> GetByMemberId(string memberId);
}
