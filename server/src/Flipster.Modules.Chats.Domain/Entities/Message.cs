using Flipster.Shared.Domain.Entities;

namespace Flipster.Modules.Chats.Domain.Entities;

public class Message : EntityBase
{
    public required string ChatId { get; set; }
    public Chat? Chat { get; set; }
    public required string FromId { get; set; }
    public required string ToId { get; set; }
    public required string Text { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
}
