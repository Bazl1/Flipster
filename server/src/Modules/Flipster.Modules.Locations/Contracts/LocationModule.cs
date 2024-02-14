using AutoMapper;
using Flipster.Modules.Locations.Dto;
using Flipster.Modules.Locations.Services;

namespace Flipster.Modules.Locations.Contracts;

public class LocationModule(
    IMapper _mapper,
    ILocationService _locationService) : ILocationModule
{
    public List<LocationDto> GetAll()
    {
        return _mapper.Map<List<LocationDto>>(_locationService.GetAll());
    }
}