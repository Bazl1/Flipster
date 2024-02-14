using Flipster.Modules.Locations.Entities;

namespace Flipster.Modules.Locations.Services;

public interface ILocationService
{
    List<Location> GetAll();
}