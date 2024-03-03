using Flipster.Modules.Catalog.Domain.Repositories;
using Flispter.Shared.Contracts.EventBus;
using Flispter.Shared.Contracts.Users.Events;

namespace Flipster.Modules.Catalog.EventHandlers;

public class UserLoggedinHandler(
    IViewRepository _viewRepository) : IEventHandler<UserLoggedinEvent>
{
    public void Handler(UserLoggedinEvent e)
    {
        _viewRepository.Update(e.VisitorId, e.UserId);
    }
}
