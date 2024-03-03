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

    public void Update(string visitorId, string userId)
    {
        foreach (var view in _db.Views.Where(v => v.UserId == visitorId))
        {
            if (!_db.Views.Any(v => v.AdvertId == view.AdvertId && v.UserId == userId))
                _db.Add(new View { AdvertId = view.AdvertId, UserId = userId });
            _db.Remove(view);
        }
        _db.SaveChanges();
    }
}
