using Flipster.Modules.Identity.Domain.Token.Repositories;

namespace Flipster.Modules.Identity.Infrastructure.Persistance.Repositories;

public class TokenRepository(
    IdentityDbContext _db) : ITokenRepository
{
    public void CreateOrUpdate(Domain.Token.Entities.Token token)
    {
        if (_db.Tokens.SingleOrDefault(t => t.UserId == token.UserId) is not Domain.Token.Entities.Token tokenEntity)
        {
            _db.Add(token);
        }
        else
        {
            tokenEntity.Value = token.Value;
            tokenEntity.Expiry = token.Expiry;
        }
        _db.SaveChanges();
    }

    public void Remove(Domain.Token.Entities.Token token)
    {
        if (_db.Tokens.SingleOrDefault(t => t.UserId == token.UserId) is not Domain.Token.Entities.Token tokenEntity)
        {
            return;
        }
        else
        {
            tokenEntity.Value = null;
            tokenEntity.Expiry = null;
        }
        _db.SaveChanges();
    }

    public Domain.Token.Entities.Token? FindByUserId(string userId)
    {
        return _db.Tokens.SingleOrDefault(t => t.UserId == userId);
    }

    public Domain.Token.Entities.Token? FindByValue(string value)
    {
        return _db.Tokens.SingleOrDefault(t => t.Value == value);
    }
}