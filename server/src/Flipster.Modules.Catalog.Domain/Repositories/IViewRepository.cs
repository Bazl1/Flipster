using Flipster.Modules.Catalog.Domain.Entities;
using Flipster.Shared.Domain.Repositories;

namespace Flipster.Modules.Catalog.Domain.Repositories;

public interface IViewRepository : IRepository<View>
{
    int GetCountByUserId(string userId);
    int GetCountByAdvertId(string advertId);
    void Update(string visitorId, string userId);
}
