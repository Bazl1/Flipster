#pragma warning disable CS8601

using Flipster.Identity.Core.Domain.Entities;

namespace Flipster.Identity.Core.Dtos;

public class LocationDto
{
    public int Value { get; set; }
    public string Label { get; set; } = null!;

    public static LocationDto From(Location location)
        => new LocationDto
        {
            Value = location.Id,
            Label = location.Value,
        };
}