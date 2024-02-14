using System.Text.Json;
using Flipster.Modules.Locations.Entities;
using Microsoft.AspNetCore.Hosting;

namespace Flipster.Modules.Locations.Services;

public class LocationService(
    IWebHostEnvironment _webHostEnvironment) : ILocationService
{
    private const string UA_LOCATION = "ua.json";
    
    public List<Location> GetAll()
    {
        string locationsJsonString = File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, UA_LOCATION));
        var locations = JsonSerializer.Deserialize<List<Location>>(locationsJsonString, options: new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
        return locations;
    }
}