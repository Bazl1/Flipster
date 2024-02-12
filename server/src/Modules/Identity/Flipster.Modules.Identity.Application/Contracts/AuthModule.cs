using System.Security.Claims;
using AutoMapper;
using Flipster.Modules.Identity.Application.Contracts.Abstractions;
using Flipster.Modules.Identity.Application.Dtos;
using Flipster.Modules.Identity.Application.Dtos.SignIn;
using Flipster.Modules.Identity.Application.Dtos.SignUp;
using Flipster.Modules.Identity.Application.Helpers;
using Flipster.Modules.Identity.Domain.Common;
using Flipster.Modules.Identity.Domain.Token.Entities;
using Flipster.Modules.Identity.Domain.Token.Errors;
using Flipster.Modules.Identity.Domain.Token.Repositories;
using Flipster.Modules.Identity.Domain.Token.Services;
using Flipster.Modules.Identity.Domain.User.Entities;
using Flipster.Modules.Identity.Domain.User.Errors;
using Flipster.Modules.Identity.Domain.User.Repositories;
using Flipster.Modules.Identity.Domain.User.Rules;
using Flipster.Modules.Identity.Domain.User.Services;
using Microsoft.AspNetCore.Http;

namespace Flipster.Modules.Identity.Application.Contracts;

public class AuthModule(
    UniqueEmailAddressRule _uniqueEmailAddressRule,
    UniquePhoneNumberAddressRule _uniquePhoneNumberAddressRule,
    IUserRepository _userRepository,
    IPasswordHasher _passwordHasher,
    IAuthService _authService,
    ITokenGenerator _tokenGenerator,
    ITokenRepository _tokenRepository,
    IHttpContextAccessor _httpContextAccessor,
    IMapper _mapper
) : IAuthModule
{
    public IdentityResult<AuthDto> Refresh()
    {
        var tokenValue = CookieHelper.GetToken(_httpContextAccessor);
        if (_tokenRepository.FindByValue(tokenValue) is not Token token)
            return IdentityResult<AuthDto>.Failed(new InvalidTokenError());
        if (token.Expired())
            return IdentityResult<AuthDto>.Failed(new TokenExpiredError());
        var user = _userRepository.FindById(token.UserId);
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
        };
        var accessToken = _tokenGenerator.GenerateAccessToken(claims);
        var refreshToken = _tokenGenerator.GenerateRefreshToken();
        _tokenRepository.CreateOrUpdate(refreshToken);
        return IdentityResult<AuthDto>.Secceeded(new AuthDto
        {
            AccessToken = accessToken,
            User = _mapper.Map<UserDto>(user)
        });
    }

    public IdentityResult<AuthDto> SignIn(SignInInputDto input)
    {
        if (_userRepository.FindByEmail(input.Email) is not User user)
            return IdentityResult<AuthDto>.Failed(new UserWithThisEmailNotFoundError(input.Email));
        if (_passwordHasher.VerifyHashedPassword(user.PasswordHash, input.Password))
            return IdentityResult<AuthDto>.Failed(new PasswordMismatchError());
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
        };
        var accessToken = _tokenGenerator.GenerateAccessToken(claims);
        var refreshToken = _tokenGenerator.GenerateRefreshToken();
        _tokenRepository.CreateOrUpdate(refreshToken);
        return IdentityResult<AuthDto>.Secceeded(new AuthDto
        {
            AccessToken = accessToken,
            User = _mapper.Map<UserDto>(user)
        });
    }

    public IdentityResult SignOut()
    {
        CookieHelper.RemoveToken(_httpContextAccessor);
        return IdentityResult.Secceeded();
    }

    public IdentityResult<AuthDto> SignUp(SignUpInputDto input)
    {
        var user = new User
        {
            Name = input.Name,
            Email = input.Email,
            PhoneNumber = input.PhoneNumber,
        };
        if (!_uniqueEmailAddressRule.Validate(user))
            return IdentityResult<AuthDto>.Failed(new DuplicateEmailError(user.Email));
        if (!_uniquePhoneNumberAddressRule.Validate(user))
            return IdentityResult<AuthDto>.Failed(new DuplicatePhoneNumberError(user.PhoneNumber));
        _userRepository.Create(user);
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
        };
        var accessToken = _tokenGenerator.GenerateAccessToken(claims);
        var refreshToken = _tokenGenerator.GenerateRefreshToken();
        _tokenRepository.CreateOrUpdate(refreshToken);
        CookieHelper.AddToken(_httpContextAccessor, refreshToken);
        return IdentityResult<AuthDto>.Secceeded(new AuthDto
        {
            AccessToken = accessToken,
            User = _mapper.Map<UserDto>(user)
        });
    }
}