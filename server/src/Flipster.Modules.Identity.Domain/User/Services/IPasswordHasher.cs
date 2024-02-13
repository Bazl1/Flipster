namespace Flipster.Modules.Identity.Domain.User.Services;

public interface IPasswordHasher
{
    string Hash(string password);
    bool VerifyHashedPassword(string passwordHash, string password);
}