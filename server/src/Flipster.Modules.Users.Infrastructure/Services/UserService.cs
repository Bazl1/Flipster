using Flipster.Modules.Users.Domain.Entities;
using Flipster.Modules.Users.Domain.Enums;
using Flipster.Modules.Users.Domain.Repositories;
using Flipster.Modules.Users.Domain.Services;
using Flipster.Shared.Domain.Errors;

namespace Flipster.Modules.Users.Infrastructure.Services;

internal class UserService(
    IPasswordHasher _passwordHasher,
    ILocationRepository _locationRepository) : IUserService
{
    public void ChangePassword(User user, string currentPassword, string newPassword)
    {
        if (!_passwordHasher.VerifyPassword(user.PasswordHash, currentPassword))
        {
            throw new FlipsterError("Password mismatch");
        }
        user.PasswordHash = _passwordHasher.HashPassword(newPassword);
    }

    public void ChangeRole(string id, UserRole role)
    {
        throw new NotImplementedException();
    }

    public void Update(User user, string? name = null, string? avatar = null, string? phoneNumber = null, string? locationValue = null)
    {
        if (name != null)
            user.Name = name;
        if (avatar != null)
            user.Avatar = avatar;
        if (phoneNumber != null)
            user.PhoneNumber = phoneNumber;
        if (locationValue != null && locationValue != string.Empty)
        {
            if (_locationRepository.GetByValue(locationValue) is not Location location)
                throw new FlipsterError("Invalid location.");
            user.LocationId = location.Id;
        }
    }
}
