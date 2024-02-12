namespace Flipster.Modules.Identity.Application.Dtos;

public class AuthDto
{
    public string AccessToken { get; set; } = null!;
    public UserDto User { get; set; } = null!;
}