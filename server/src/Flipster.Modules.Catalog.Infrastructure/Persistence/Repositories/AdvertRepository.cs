using Flipster.Modules.Catalog.Domain.Entities;
using Flipster.Modules.Catalog.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Flipster.Modules.Catalog.Infrastructure.Persistence.Repositories;

public class AdvertRepository(
    CatalogModuleContext _db) : IAdvertRepository
{
    public void Add(Advert entity)
    {
        _db.Add(entity);
        _db.SaveChanges();
    }

    public IEnumerable<Advert> GetAll()
    {
        return _db.Adverts;
    }

    public Advert? GetById(string id)
    {
        return _db.Adverts
            .Include(advert => advert.Category)
            .SingleOrDefault(advert => advert.Id == id);
    }

    public IEnumerable<Advert> GetByUserId(string userId)
    {
        return _db.Adverts
            .Include(advert => advert.Category)
            .Where(advert => advert.SellerId == userId);
    }

    public void Remove(Advert entity)
    {
        _db.Remove(entity);
        _db.SaveChanges();
    }

    public IEnumerable<Advert> Search(string? query = null, int? min = null, int? max = null, string? categoryId = null, string? location = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Advert entity)
    {
        _db.SaveChanges();
    }
}
