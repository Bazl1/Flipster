using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Flipster.Identity.Core.Domain.Entities;

public class Location
{
    public int Id { get; set; }
    public string Value { get; set; } = null!;

    public Location(string value)
    {
        Value = value;
    }

    [NotMapped]
    public string Country { get => GetCountry(); }
    [NotMapped]
    public string State { get => GetState(); }
    [NotMapped]
    public string City { get => GetCity(); }

    public string GetCountry()
    {
        if (string.IsNullOrEmpty(Value))
        {
            return string.Empty;
        }
        var parts = Value.Split(",").Select(part => part.Trim()).ToList();
        if (parts.Count < 1)
        {
            throw new Exception("Invalid lovation");
        }
        return parts[0];
    }

    public string GetState()
    {
        if (string.IsNullOrEmpty(Value))
        {
            return string.Empty;
        }
        var parts = Value.Split(",").Select(part => part.Trim()).ToList();
        if (parts.Count < 2)
        {
            throw new Exception("Invalid lovation");
        }
        return parts[1];
    }

    public string GetCity()
    {
        if (string.IsNullOrEmpty(Value))
        {
            return string.Empty;
        }
        var parts = Value.Split(",").Select(part => part.Trim()).ToList();
        if (parts.Count < 3)
        {
            throw new Exception("Invalid lovation");
        }
        return parts[2];
    }
}