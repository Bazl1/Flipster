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
        return _db.Adverts
            .Include(a => a.Category);
    }

    public IEnumerable<Advert> GetByCategoryId(string categoryId)
    {
        return _db.Adverts
            .Where(a => a.CategoryId == categoryId)
            .Include(a => a.Category);
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

    public IEnumerable<Advert> Search(string query = "", int min = -1, int max = -1, string categoryId = "", string location = "")
    {
        return _db.Adverts
            .Where(advert =>
                (advert.Status == Domain.Enums.Status.Active) &&
                (query == "" || (advert.Title.ToUpper().Contains(query.ToUpper()) || advert.Description.ToUpper().Contains(query.ToUpper()))) &&
                (min == -1 || min <= advert.Price) &&
                (max == -1 || max >= advert.Price) &&
                (categoryId == "" || advert.CategoryId == categoryId) &&
                (location == "" || advert.Location == location))
            .Include(a => a.Category);
    }

    public void Update(Advert entity)
    {
        _db.SaveChanges();
    }
}
