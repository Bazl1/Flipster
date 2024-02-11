using Flipster.Identity.Core.Data;
using Flipster.Identity.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Flipster.Identity.Api.Controllers;

[Route("[controller]")]
public class LocationsController(
    ApplicationDbContext _db
) : ApiController
{
    [HttpGet()]
    public async Task<List<LocationDto>> GetLocations()
    {
        return _db.Locations
            .Select(location => LocationDto.From(location))
            .ToList();
    }
}