using Flipster.Shared.Contracts.Catalog.Dtos;

namespace Flipster.Modules.Catalog.Dtos;

public class ContactDto : IContactDto
{
    public required string Id { get; set; }
    public required string Avatar { get; set; }
    public required string Name { get; set; }
    public required string Location { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
}
