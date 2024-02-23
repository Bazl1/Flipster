namespace Flipster.Modules.Users.Domain.Entities;

public class View
{
    public required string UserId { get; set; }
    public User? User { get; set; }

    public required string AdvertId { get; set; }
}
