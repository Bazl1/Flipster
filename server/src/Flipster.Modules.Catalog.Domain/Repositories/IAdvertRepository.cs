using Flipster.Modules.Catalog.Domain.Entities;
using Flipster.Shared.Domain.Repositories;

namespace Flipster.Modules.Catalog.Domain.Repositories;

public interface IAdvertRepository : IRepository<Advert>
{
    IEnumerable<Advert> Search(string query = "", int min = -1, int max = -1, string categoryId = "", string location = "");
    IEnumerable<Advert> GetByUserId(string userId);
    IEnumerable<Advert> GetByCategoryId(string categoryId);
}
