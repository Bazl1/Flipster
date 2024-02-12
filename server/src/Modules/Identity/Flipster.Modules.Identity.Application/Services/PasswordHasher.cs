using Flipster.Modules.Identity.Domain.User.Services;

namespace Flipster.Modules.Identity.Application.Services;

internal class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        throw new NotImplementedException();
    }

    public bool VerifyHashedPassword(string passwordHash, string password)
    {
        throw new NotImplementedException();
    }
}