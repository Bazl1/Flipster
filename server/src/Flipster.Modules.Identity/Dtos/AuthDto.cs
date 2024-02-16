namespace Flipster.Modules.Identity.Dtos;

public class AuthDto
{
    public string AccessToken { get; set; } = null!;
    public string? AntiforgeryToken { get; set; } = null;
    public UserDto User { get; set; } = null!;
}