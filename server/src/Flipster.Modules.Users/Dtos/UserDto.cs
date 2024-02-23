namespace Flipster.Modules.Users.Dtos;

public class UserDto
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Location { get; set; }
    public required string Avatar { get; set; }
}