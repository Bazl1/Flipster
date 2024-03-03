namespace Flipster.Modules.Chats.Dtos;

public class MessageDto
{
    public string Id { get; set; }
    public UserDto From { get; set; }
    public string Text { get; set; }
    public string CreatedAt { get; set; }
    public bool IsRead { get; set; }
    public bool IsDeleted { get; set; }
}