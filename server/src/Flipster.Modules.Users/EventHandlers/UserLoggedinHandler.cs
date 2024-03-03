using Flipster.Modules.Users.Domain.Repositories;
using Flispter.Shared.Contracts.EventBus;
using Flispter.Shared.Contracts.Users.Events;

namespace Flipster.Modules.Users.EventHandlers;

public class UserLoggedinHandler(
    IFavoriteRepository _favoriteRepository) : IEventHandler<UserLoggedinEvent>
{
    public void Handler(UserLoggedinEvent e)
    {
        _favoriteRepository.Update(e.VisitorId, e.UserId);
    }
}
