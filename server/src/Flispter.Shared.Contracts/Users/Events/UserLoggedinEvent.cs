using Flispter.Shared.Contracts.EventBus;

namespace Flispter.Shared.Contracts.Users.Events;

public class UserLoggedinEvent : IEvent
{
    public string UserId { get; set; }
    public string VisitorId { get; set; }

    public UserLoggedinEvent(string userId, string visitorId)
    {
        UserId = userId;
        VisitorId = visitorId;
    }
}