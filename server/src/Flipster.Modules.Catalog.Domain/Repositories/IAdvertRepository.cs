using Flipster.Modules.Catalog.Domain.Entities;
using Flipster.Shared.Domain.Repositories;

namespace Flipster.Modules.Catalog.Domain.Repositories;

public interface IAdvertRepository : IRepository<Advert>
{
    IEnumerable<Advert> Search(string? query = null, int? min = null, int? max = null, bool isFree = false, string? categoryId = null, string? location = null); // TODO: chane to search system.
    IEnumerable<Advert> GetByUserId(string userId);
}
