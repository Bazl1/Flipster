using Flipster.Modules.Users.Domain.Entities;
using Flipster.Modules.Users.Domain.Enums;

namespace Flipster.Modules.Users.Domain.Services;

public interface IUserService
{
    void Update(User user, string? name = null, string? avatar = null, string? phoneNumber = null, string? location = null);
    void ChangePassword(User user, string currentPassword, string newPassword);
    void ChangeRole(string id, UserRole role);
}