using Flipster.Modules.Chats.Domain.Entities;
using Flipster.Modules.Chats.Domain.Repositories;

namespace Flipster.Modules.Chats.Infrastructure.Persistence.Repositories;

public class MessageRepository(
    ChatsModuleDbContext _db) : IMessageRepository
{
    public void Add(Message entity)
    {
        _db.Add(entity);
        _db.SaveChanges();
    }

    public IEnumerable<Message> GetAll()
    {
        return _db.Messages.OrderByDescending(m => m.CreatedAt);
    }

    public IEnumerable<Message> GetByChatId(string chatId)
    {
        throw new NotImplementedException();
    }

    public Message? GetById(string id)
    {
        return _db.Messages.SingleOrDefault(m => m.Id == id);
    }

    public IEnumerable<Message> GetByMemberId(string memberId)
    {
        return _db.Messages
            .Where(m => m.FromId == memberId || m.ToId == memberId)
            .OrderByDescending(m => m.CreatedAt);
    }

    public int GetUnreadMessagesCount(string chatId, string memberId)
    {
        return _db.Messages
            .Where(m => m.ChatId == chatId && m.ToId == memberId && !m.IsRead)
            .Count();
    }

    public void Remove(Message entity)
    {
        _db.Remove(entity);
        _db.SaveChanges();
    }

    public void Update(Message entity)
    {
        _db.SaveChanges();
    }
}
