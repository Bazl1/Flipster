using System.ComponentModel.DataAnnotations;

namespace Flipster.Shared.Domain.Entities;

public class EntityBase
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
}