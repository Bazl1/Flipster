using Flipster.Modules.Users.Domain.Entities;
using Flipster.Shared.Domain.Repositories;

namespace Flipster.Modules.Users.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    User? GetByEmail(string email);
    IEnumerable<User> GetByLocation(string location);
    IEnumerable<Query> GetByQueryHistoryById(string id);
    IEnumerable<View> GetViewsById(string id);
    IEnumerable<User> GetAll();
}
