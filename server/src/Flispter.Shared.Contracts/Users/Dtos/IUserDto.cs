namespace Flispter.Shared.Contracts.Users.Dtos;

public interface IUserDto
{
    string Id { get; set; }
    string Name { get; set; }
    string Email { get; set; }
    string Role { get; set; }
    string PhoneNumber { get; set; }
    string Location { get; set; }
    string Avatar { get; set; }
}