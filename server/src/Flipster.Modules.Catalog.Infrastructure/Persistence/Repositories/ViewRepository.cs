using Flipster.Modules.Catalog.Domain.Entities;
using Flipster.Modules.Catalog.Domain.Repositories;

namespace Flipster.Modules.Catalog.Infrastructure.Persistence.Repositories;

internal class ViewRepository(
    CatalogModuleContext _db) : IViewRepository
{
    public void Add(View entity)
    {
        _db.Add(entity);
        _db.SaveChanges();
    }

    public IEnumerable<View> GetAll()
    {
        throw new NotImplementedException();
    }

    public View? GetById(string id)
    {
        throw new NotImplementedException();
    }

    public int GetCountByAdvertId(string advertId)
    {
        return _db.Views
            .Where(v => v.AdvertId == advertId)
            .Count();
    }

    public int GetCountByUserId(string userId)
    {
        return _db.Views
            .Where(v => v.UserId == userId)
            .Count();
    }

    public void Remove(View entity)
    {
        _db.Remove(entity);
        _db.SaveChanges();
    }

    public void Update(View entity)
    {
        _db.SaveChanges();
    }
}
