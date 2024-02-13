namespace Flipster.Modules.Identity.Dtos.Login;

public record LoginRequestDto(
    string Email,
    string Password);