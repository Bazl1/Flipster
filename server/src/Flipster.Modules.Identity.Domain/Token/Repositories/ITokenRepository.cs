namespace Flipster.Modules.Identity.Domain.Token.Repositories;

public interface ITokenRepository
{
    void CreateOrUpdate(Entities.Token token);
    void Remove(Entities.Token token);
    Entities.Token? FindByUserId(string userId);
    Entities.Token? FindByValue(string value);
}