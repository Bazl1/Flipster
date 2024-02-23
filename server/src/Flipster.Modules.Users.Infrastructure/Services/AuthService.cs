using Flipster.Modules.Users.Domain.Entities;
using Flipster.Modules.Users.Domain.Repositories;
using Flipster.Modules.Users.Domain.Services;
using Flipster.Shared.Domain.Errors;

namespace Flipster.Modules.Users.Infrastructure.Services;

internal class AuthService(
    IUserRepository _userRepository,
    ITokenRepository _tokenRepository,
    IPasswordHasher _passwordHasher) : IAuthService
{
    public User Login(string email, string password)
    {
        if (_userRepository.GetByEmail(email) is not User user)
            throw new FlipsterError("User with given email is not found.");
        if (!_passwordHasher.VerifyPassword(user.PasswordHash, password))
            throw new FlipsterError("Password mismatch.");
        return user;
    }

    public void Logout(User user)
    {
        if (_tokenRepository.GetById(user.Id) is not Token token)
            throw new FlipsterError("Invalid token.");
        if (DateTime.UtcNow >= token.ExpiryIn)
            throw new FlipsterError("Token is expired.");
        _tokenRepository.Remove(token);
    }

    public void Refresh(User user, string tokenValue)
    {
        var token = _tokenRepository.GetById(user.Id);
        if (token == null)
        {
            var newToken = new Token
            {
                UserId = user.Id,
                Value = tokenValue,
                ExpiryIn = DateTime.UtcNow.AddMinutes(10)
            };
            _tokenRepository.Add(newToken);
            return;
        }
        if (DateTime.UtcNow >= token.ExpiryIn)
            throw new FlipsterError("Invalid token.");
        token.Value = tokenValue;
        token.ExpiryIn = DateTime.UtcNow.AddMinutes(10);
        _tokenRepository.Update(token);
    }

    public void Register(User user, string password)
    {
        if (_userRepository.GetByEmail(user.Email) is not null)
            throw new FlipsterError("User with given email is already exists.");
        user.PasswordHash = _passwordHasher.HashPassword(password);
        _userRepository.Add(user);
    }
}
