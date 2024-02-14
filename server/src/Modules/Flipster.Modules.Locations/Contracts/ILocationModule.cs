using Flipster.Modules.Locations.Dto;

namespace Flipster.Modules.Locations.Contracts;

public interface ILocationModule
{
    List<LocationDto> GetAll();
}