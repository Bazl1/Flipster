namespace Flipster.Modules.Identity.Dtos.Register;

public record RegisterRequestDto(
    string Name,
    string Email,
    string Password);