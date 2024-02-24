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
            .Include(a => a.Category)
            .SingleOrDefault(a => a.Id == id);
    }

    public IEnumerable<Advert> GetByUserId(string userId)
    {
        return _db.Adverts
            .Where(a => a.SellerId == userId)
            .Include(a => a.Category);
    }

    public void Remove(Advert entity)
    {
        _db.Remove(entity);
        _db.SaveChanges();
    }

    public IEnumerable<Advert> Search(string? query = null, int min = -1, int max = -1, string? categoryId = null, string? location = null)
    {
        return _db.Adverts
            .Where(advert =>
                (query == null || (advert.Title.Contains(query) || advert.Description.Contains(query))) &&
                ((min == -1 || max == -1) || (min <= advert.Price && advert.Price <= max)) &&
                (categoryId == null || advert.CategoryId == categoryId) &&
                (location == null || advert.Location == location))
            .Include(a => a.Category);
    }

    public void Update(Advert entity)
    {
        _db.SaveChanges();
    }
}
