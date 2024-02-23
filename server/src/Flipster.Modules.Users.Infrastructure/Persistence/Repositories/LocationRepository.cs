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
        throw new NotImplementedException();
    }

    public Location? GetById(string id)
    {
        throw new NotImplementedException();
    }

    public Location? GetByValue(string value)
    {
        throw new NotImplementedException();
    }

    public void Remove(Location entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Location entity)
    {
        throw new NotImplementedException();
    }
}
