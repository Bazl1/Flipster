using Flipster.Modules.Users.Domain.Entities;
using Flipster.Modules.Users.Domain.Repositories;

namespace Flipster.Modules.Users.Infrastructure.Persistence.Repositories;

internal class LocationRepository(
    UsersModuleDbContext _db) : ILocationRepository
{
    public void Add(Location entity)
    {
        _db.Add(entity);
        _db.SaveChanges();
    }

    public IEnumerable<Location> GetAll()
    {
        return _db.Locations;
    }

    public Location? GetById(string id)
    {
        return _db.Locations.SingleOrDefault(location => location.Id == id);
    }

    public Location? GetByValue(string value)
    {
        return _db.Locations.SingleOrDefault(location => location.Value == value);
    }

    public void Remove(Location entity)
    {
        _db.Remove(entity);
        _db.SaveChanges();
    }

    public void Update(Location entity)
    {
        _db.SaveChanges();
    }
}
