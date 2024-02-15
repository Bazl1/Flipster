namespace Flipster.Modules.Adverts.ValueObjects;

public record AdvertPrice(
    bool IsFree,
    decimal Value);