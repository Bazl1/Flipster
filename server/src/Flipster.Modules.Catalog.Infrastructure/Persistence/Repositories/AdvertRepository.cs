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

    public IEnumerable<Advert> Search(string? query = null, int? min = null, int? max = null, string? categoryId = null, string? location = null)
    {
        List<string> keyWords = new();
        if (query != null)
            if (query.Trim().Contains(' '))
                keyWords.AddRange(query.Split().ToList());
            else
                keyWords.Add(query);
        return _db.Adverts
            .Where(advert =>
                (query == null || keyWords.Any(keyWord => advert.Title.Contains(keyWord) || advert.Description.Contains(keyWord))) &&
                ((min == null || max == null) || (min <= advert.Price && advert.Price <= max)) &&
                (categoryId == null || advert.CategoryId == categoryId) &&
                (location == null || advert.Location == location))
            .Include(a => a.Category);
    }

    public void Update(Advert entity)
    {
        _db.SaveChanges();
    }
}
