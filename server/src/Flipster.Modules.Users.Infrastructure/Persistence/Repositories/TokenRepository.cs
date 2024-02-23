using Flipster.Modules.Users.Domain.Entities;
using Flipster.Modules.Users.Domain.Repositories;

namespace Flipster.Modules.Users.Infrastructure.Persistence.Repositories;

internal class TokenRepository(
    UsersModuleDbContext _db) : ITokenRepository
{
    public void Add(Token entity)
    {
        _db.Add(entity);
        _db.SaveChanges();
    }

    public IEnumerable<Token> GetAll()
    {
        return _db.Tokens;
    }

    public Token? GetById(string id)
    {
        return _db.Tokens.SingleOrDefault(t => t.UserId == id);
    }

    public Token? GetByValue(string value)
    {
        return _db.Tokens.SingleOrDefault(t => t.Value == value);
    }

    public void Remove(Token entity)
    {
        _db.Tokens.Remove(entity);
        _db.SaveChanges();
    }

    public void Update(Token entity)
    {
        _db.SaveChanges();
    }
}
