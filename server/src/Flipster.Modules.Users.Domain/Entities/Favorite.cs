using System.ComponentModel.DataAnnotations;

namespace Flipster.Modules.Users.Domain.Entities;

public class Favorite
{
    [Key]
    public required string UserId { get; set; }
    public User? User { get; set; }

    public required string AdvertId { get; set; }
}