using System.ComponentModel.DataAnnotations;

namespace Flipster.Identity.Core.Domain.Entities;

public class RefreshToken
{
    [Key]
    public string UserId { get; set; } = null!;
    public string Value { get; set; } = null!;
    public DateTime Expiry { get; set; }

    public static RefreshToken Create(string userId, string value, int expiryMinutes)
        => new RefreshToken
        {
            UserId = userId,
            Value = value,
            Expiry = DateTime.UtcNow.AddMinutes(expiryMinutes)
        };
}