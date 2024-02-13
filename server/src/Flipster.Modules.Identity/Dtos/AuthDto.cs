namespace Flipster.Modules.Identity.Dtos;

public class AuthDto
{
    public string AccessToken { get; set; }
    public UserDto User { get; set; }
}