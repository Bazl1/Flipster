namespace Flipster.Modules.Identity.Application.Dtos.SignUp;

public record SignUpInputDto(
    string Name,
    string Email,
    string PhoneNumber,
    string Password
);