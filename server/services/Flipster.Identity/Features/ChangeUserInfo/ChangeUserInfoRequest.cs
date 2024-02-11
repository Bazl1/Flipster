namespace Flipster.Identity.Features.ChangeUserInfo;

public record ChangeUserInfoRequest(
    string Avatar,
    string Name,
    string PhoneNumber
);