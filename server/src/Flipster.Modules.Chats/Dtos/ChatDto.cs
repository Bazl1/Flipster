namespace Flipster.Modules.Chats.Dtos;

public class ChatDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public int UnreadMessageCount { get; set; }
    public UserDto Interlocutor { get; set; }
}
