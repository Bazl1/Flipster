using Flipster.Shared.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Flipster.Modules.Users.Domain.Entities;

public class Token : IAggregate
{
    [Key]
    public required string UserId { get; set; }
    public User? User { get; set; }

    public required string Value { get; set; }
    public required DateTime ExpiryIn { get; set; }
}
