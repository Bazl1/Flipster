using Flipster.Modules.Users.Domain.Entities;
using Flipster.Shared.Domain.Repositories;

namespace Flipster.Modules.Users.Domain.Repositories;

public interface IFavoriteRepository : IRepository<Favorite>
{
    IEnumerable<Favorite> GetByUserId(string userId);
    Favorite? GetByUserIdAndAdvertId(string userId, string advertId);
}
