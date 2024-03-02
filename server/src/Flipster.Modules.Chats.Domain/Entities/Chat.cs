using Flipster.Shared.Domain.Entities;
using Flipster.Shared.Domain.Errors;

namespace Flipster.Modules.Chats.Domain.Entities;

public class Chat : EntityBase, IAggregateRoot
{
    public required string Title { get; set; }
    public required string FirstMemberId { get; set; }
    public required string SecondMemberId { get; set; }

    public List<Message>? Messages { get; set; }

    public int GetUnredMessagesCountByMemberId(string memberId)
    {
        if (FirstMemberId != memberId && SecondMemberId != memberId)
            throw new FlipsterError("The user is not a member of the chat room.");
        if (Messages == null)
            throw new FlipsterError("Messages is not found.");
        return Messages
            .Where(m => !m.IsRead && m.ToId == memberId)
            .Count();
    }

    public string GetInterlocutorByMemberId(string memberId)
    {
        return FirstMemberId == memberId ? SecondMemberId : FirstMemberId;
    }
}