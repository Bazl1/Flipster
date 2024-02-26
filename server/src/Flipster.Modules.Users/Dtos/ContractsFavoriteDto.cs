using Flispter.Shared.Contracts.Users.Dtos;

namespace Flipster.Modules.Users.Dtos;

public class ContractFavoriteDto : IFavoriteDto
{
    public string UserId { get; set; }
    public string AdvertId { get; set; }
}