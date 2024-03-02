using Flipster.Modules.Chats.Domain.Entities;
using Flipster.Modules.Chats.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Flipster.Modules.Chats.Infrastructure.Persistence.Repositories;

internal class ChatRepository(
    ChatsModuleDbContext _db) : IChatRepository
{
    public void Add(Chat entity)
    {
        _db.Add(entity);
        _db.SaveChanges();
    }

    public IEnumerable<Chat> GetAll()
    {
        return _db.Chats;
    }

    public Chat? GetById(string id)
    {
        return _db.Chats
            .Include(c => c.Messages)
            .SingleOrDefault(c => c.Id == id);
    }

    public IEnumerable<Chat> GetByMemberId(string memberId)
    {
        return _db.Chats
            .Include(c => c.Messages)
            .Where(c => c.FirstMemberId == memberId || c.SecondMemberId == memberId);
    }

    public void Remove(Chat entity)
    {
        _db.Remove(entity);
        _db.SaveChanges();
    }

    public void Update(Chat entity)
    {
        _db.SaveChanges();
    }
}
