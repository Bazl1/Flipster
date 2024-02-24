namespace Flipster.Modules.Users.Endpoints.Favorites.Synchronizing;

public record Request(
    IEnumerable<string> Ids);