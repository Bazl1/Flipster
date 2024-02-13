using System.Security.Claims;
using AutoMapper;
using Flipster.Modules.Identity.Domain.Token.Entities;
using Flipster.Modules.Identity.Domain.Token.Repositories;
using Flipster.Modules.Identity.Domain.Token.Services;
using Flipster.Modules.Identity.Domain.User.Entities;
using Flipster.Modules.Identity.Domain.User.Repositories;
using Flipster.Modules.Identity.Domain.User.Services;
using Flipster.Modules.Identity.Dtos;
using Flipster.Modules.Identity.Dtos.Login;
using Flipster.Modules.Identity.Dtos.Register;
using Flipster.Modules.Identity.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Flipster.Modules.Identity.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/login", Login);
        builder.MapPost("/register", Register);
        builder.MapPost("/logout", Logout).RequireAuthorization();
        builder.MapPost("/refresh", Refresh);
        
        return builder;
    }

    private static async Task<IResult> Login(
        HttpContext context,
        IUserRepository userRepository,
        ITokenRepository tokenRepository,
        ITokenGenerator tokenGenerator,
        IPasswordHasher passwordHasher,
        IMapper mapper,
        LoginRequestDto request)
    {
        if (userRepository.FindByEmail(request.Email) is not User user)
            return TypedResults.BadRequest(new ErrorDto($"User with given email '{request.Email}' is not found."));
        if (!passwordHasher.VerifyHashedPassword(user.PasswordHash, request.Password))
            return TypedResults.BadRequest(new ErrorDto($"Password mismatch."));
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
        };
        var accessToken = tokenGenerator.GenerateAccessToken(claims);
        var refreshToken = tokenGenerator.GenerateRefreshToken();
        refreshToken.UserId = user.Id;
        tokenRepository.CreateOrUpdate(refreshToken);
        return TypedResults.Ok(new AuthDto
        {
            AccessToken = accessToken,
            User = mapper.Map<UserDto>(user)
        });
    }

    private static async Task<IResult> Register(
        HttpContext context,
        IUserRepository userRepository,
        ITokenRepository tokenRepository,
        ITokenGenerator tokenGenerator,
        IPasswordHasher passwordHasher,
        IMapper mapper,
        RegisterRequestDto request)
    {
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
        };
        if (userRepository.FindByEmail(request.Email) is not null)
            return TypedResults.BadRequest(new ErrorDto($"User with this email '{request.Email}' already exists."));
        if (userRepository.FindByPhoneNumber(request.PhoneNumber) is not null)
            return TypedResults.BadRequest(new ErrorDto($"User with this phone number '{request.PhoneNumber}' already exists."));
        user.PasswordHash = passwordHasher.Hash(request.Password);
        userRepository.Create(user);
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
        };
        var accessToken = tokenGenerator.GenerateAccessToken(claims);
        var refreshToken = tokenGenerator.GenerateRefreshToken();
        refreshToken.UserId = user.Id;
        tokenRepository.CreateOrUpdate(refreshToken);
        CookieUtils.AddToken(context, refreshToken);
        return TypedResults.Ok(new AuthDto
        {
            AccessToken = accessToken,
            User = mapper.Map<UserDto>(user)
        });
    }

    private static async Task<IResult> Logout(
        HttpContext context,
        [FromServices] ITokenRepository tokenRepository)
    {
        var tokenValue = CookieUtils.GetToken(context);
        if (tokenRepository.FindByValue(tokenValue) is not Token token)
            return TypedResults.Unauthorized();
        tokenRepository.Remove(token);
        CookieUtils.RemoveToken(context);
        return TypedResults.Ok();
    }

    private static async Task<IResult> Refresh(
        HttpContext context,
        [FromServices] IUserRepository userRepository,
        [FromServices] ITokenRepository tokenRepository,
        [FromServices] ITokenGenerator tokenGenerator,
        [FromServices] IMapper mapper)
    {
        var tokenValue = CookieUtils.GetToken(context);
        if (tokenRepository.FindByValue(tokenValue) is not Token token)
            return Results.Unauthorized();
        if (token.Expired())
            return TypedResults.Unauthorized();
        var user = userRepository.FindById(token.UserId);
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
        };
        var accessToken = tokenGenerator.GenerateAccessToken(claims);
        var refreshToken = tokenGenerator.GenerateRefreshToken();
        refreshToken.UserId = user.Id;
        tokenRepository.CreateOrUpdate(refreshToken);
        CookieUtils.AddToken(context, refreshToken);
        return TypedResults.Ok(new AuthDto
        {
            AccessToken = accessToken,
            User = mapper.Map<UserDto>(user)
        });
    }
}