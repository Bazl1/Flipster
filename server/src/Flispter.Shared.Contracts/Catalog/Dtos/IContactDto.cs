namespace Flipster.Shared.Contracts.Catalog.Dtos;

public interface IContactDto
{
    string Id { get; set; }
    string Avatar { get; set; }
    string Name { get; set; }
    string Location { get; set; }
    string Email { get; set; }
    string PhoneNumber { get; set; }
}
