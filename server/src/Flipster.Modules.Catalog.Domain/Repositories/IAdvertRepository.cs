using Flipster.Modules.Catalog.Domain.Entities;
using Flipster.Shared.Domain.Repositories;

namespace Flipster.Modules.Catalog.Domain.Repositories;

public interface IAdvertRepository : IRepository<Advert>
{
    IEnumerable<Advert> Search(string? query = null, int? min = null, int? max = null, string? categoryId = null, string? location = null);
    IEnumerable<Advert> GetByUserId(string userId);
}
