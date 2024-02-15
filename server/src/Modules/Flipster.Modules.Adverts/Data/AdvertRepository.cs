using Flipster.Modules.Adverts.Entities;
using Flipster.Modules.Adverts.Repositories;

namespace Flipster.Modules.Adverts.Data;

public class AdvertRepository(
    AdvertsDbContext _db) : IAdvertRepository
{
    public void Create(Advert advert)
    {
        _db.Add(advert);
        _db.SaveChanges();
    }
}