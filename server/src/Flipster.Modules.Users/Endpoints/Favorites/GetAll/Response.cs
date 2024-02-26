using Flipster.Shared.Contracts.Catalog.Dtos;

namespace Flipster.Modules.Users.Endpoints.Favorites.GetAll;

public class Response
{
    public IEnumerable<IAdvertDto> Adverts { get; set; }
    public int PageCount { get; set; }
}