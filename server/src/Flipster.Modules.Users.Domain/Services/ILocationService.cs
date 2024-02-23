using Flipster.Modules.Users.Domain.Entities;

namespace Flipster.Modules.Users.Domain.Services;

public interface ILocationService
{
    Location? Create(string location);

    Location? Update(string id, string location);
    
    void Remove(string location);

    Location? GetById(string id);
    IEnumerable<Location> GetAll();
}