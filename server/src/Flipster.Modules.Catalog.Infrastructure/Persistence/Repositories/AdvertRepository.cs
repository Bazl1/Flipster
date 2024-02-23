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

    public IEnumerable<Advert> Search(string? query = null, int? min = null, int? max = null, bool isFree = false, string? categoryId = null, string? location = null)
    {
        return _db.Adverts
            .Include(advert => advert.Category)
            .Where(advert =>
                (query == null || query.Trim().Split().Any(keyWord => advert.Title.Contains(keyWord) || advert.Description.Contains(keyWord))) &&
                (isFree ? advert.IsFree : (min == null || max == null) || (min <= advert.Price && advert.Price <= max)) &&
                (categoryId == null || advert.CategoryId == categoryId) &&
                (location == null || advert.Location == location));
    }

    public void Update(Advert entity)
    {
        _db.SaveChanges();
    }
}
