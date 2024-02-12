namespace Flipster.Modules.Identity.Domain.Token.Repositories;

public interface ITokenRepository
{
    void Create(Entities.Token token);
    void CreateOrUpdate(Entities.Token token);
    void Remove(string userId);
    User.Entities.User? FindUserByToken(string token);
    Entities.Token? FindTokenByUserId(string userId);
    Entities.Token? FindByValue(string value);
}