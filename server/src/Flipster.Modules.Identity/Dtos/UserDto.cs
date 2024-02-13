﻿namespace Flipster.Modules.Identity.Dtos;

public class UserDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string Avatar { get; set; } = null!;
    public string Location { get; set; } = null!;
}