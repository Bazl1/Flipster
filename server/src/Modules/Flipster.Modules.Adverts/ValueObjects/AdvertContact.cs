namespace Flipster.Modules.Adverts.ValueObjects;

public record AdvertContact(
    string SellerId,
    string Location,
    string Email,
    string PhoneNumber);