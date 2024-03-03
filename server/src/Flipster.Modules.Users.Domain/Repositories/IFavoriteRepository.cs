using Flipster.Modules.Users.Domain.Entities;
using Flipster.Shared.Domain.Repositories;

namespace Flipster.Modules.Users.Domain.Repositories;

public interface IFavoriteRepository : IRepository<Favorite>
{
    IEnumerable<Favorite> GetByUserId(string userId);
    Favorite? GetById(string userId, string advertId);
    Favorite? GetByUserIdAndAdvertId(string userId, string advertId);
    void Update(string visitorId, string userId);
}
