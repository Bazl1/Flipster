using Flipster.Modules.Users.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Flipster.Modules.Users.Infrastructure.Persistence.Seeds;

public class LocationSeed()
{
    private const string UA_LOCATION = "ua_locations.json";

    public void Seed(IServiceProvider serviceProvider)
    {
        using var db = serviceProvider.GetRequiredService<UsersModuleDbContext>();
        var webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
        string locationsJsonString = File.ReadAllText(Path.Combine(webHostEnvironment.WebRootPath, "data", UA_LOCATION));
        var locations = JsonSerializer.Deserialize<List<LocationDto>>(locationsJsonString, options: new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
        db.Locations.AddRange(locations.Select(location => new Location { Value = $"{location.Country}, {location.State}, {location.City}" }));
        db.SaveChanges();
    }

    private class LocationDto
    {
        public required string City { get; set; }
        public required string Country { get; set; }
        public required string State { get; set; }
    }
}