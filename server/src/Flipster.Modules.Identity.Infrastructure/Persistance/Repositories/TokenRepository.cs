using Flipster.Modules.Identity.Domain.User.Repositories;

namespace Flipster.Modules.Identity.Domain.Infrastructure.Persistance;

public class TokenRepository(
    IdentityDbContext _db) : ITokenRepository
{
    public void CreateOrUpdate(Domain.User.Entities.Token token)
    {
        if (_db.Tokens.SingleOrDefault(t => t.UserId == token.UserId) is not Domain.User.Entities.Token tokenEntity)
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

    public void Remove(Domain.User.Entities.Token token)
    {
        if (_db.Tokens.SingleOrDefault(t => t.UserId == token.UserId) is not Domain.User.Entities.Token tokenEntity)
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

    public Domain.User.Entities.Token? FindByUserId(string userId)
    {
        return _db.Tokens.SingleOrDefault(t => t.UserId == userId);
    }

    public Domain.User.Entities.Token? FindByValue(string value)
    {
        return _db.Tokens.SingleOrDefault(t => t.Value == value);
    }
}