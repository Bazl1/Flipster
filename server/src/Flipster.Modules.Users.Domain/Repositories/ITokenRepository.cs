using Flipster.Modules.Users.Domain.Entities;
using Flipster.Shared.Domain.Repositories;

namespace Flipster.Modules.Users.Domain.Repositories;

public interface ITokenRepository : IRepository<Token>
{
    Token? GetByValue(string value);
}
