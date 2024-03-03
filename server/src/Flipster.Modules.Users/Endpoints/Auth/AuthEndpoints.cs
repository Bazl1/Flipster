using AutoMapper;
using Flipster.Modules.Users.Domain.Entities;
using Flipster.Modules.Users.Domain.Enums;
using Flipster.Modules.Users.Domain.Repositories;
using Flipster.Modules.Users.Domain.Services;
using Flipster.Modules.Users.Dtos;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using Flipster.Modules.Users.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Flispter.Shared.Contracts.Catalog;

namespace Flipster.Modules.Users.Endpoints.Auth;

public static class AuthEndpoints
{
    public const string RefreshTokenCookieName = "Flipster.Authentication.RefreshToken";

    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/visit", (Delegate)VisitAsync);
        builder.MapPost("/register", (Delegate)RegisterAsync);
        builder.MapPost("/login", (Delegate)LoginAsync);
        builder.MapPost("/refresh", (Delegate)RefreshAsync);
        builder.MapPost("/logout", (Delegate)LogoutAsync);

        return builder;
    }

    private static async Task<IResult> VisitAsync(
        HttpContext context)
    {
        await context.SignInAsync(
            FlipsterAuthenticationSchemes.CookieScheme.SchemeName,
            new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, UserRole.Visitor.ToString())
                },
                FlipsterAuthenticationSchemes.CookieScheme.SchemeName)));
        return Results.Ok();
    }

    [Authorize(AuthenticationSchemes = FlipsterAuthenticationSchemes.CookieScheme.SchemeName)]
    private static async Task<IResult> RegisterAsync(
        HttpContext context,
        [FromServices] IAntiforgery antiforgery,
        [FromServices] ITokenGenerator tokenGenerator,
        [FromServices] IAuthService authService,
        [FromServices] IMapper mapper,
        [FromBody] Register.Request request)
    {
        var visitorId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = new User
        { 
            Id = visitorId,
            Name = request.Name,
            Email = request.Email
        };
        authService.Register(user, request.Password);
        var refreshToken = tokenGenerator.GenerateRefreshToken();
        authService.Refresh(user, refreshToken);
        context.Response.Cookies.Append(RefreshTokenCookieName, refreshToken);
        var accessToken = tokenGenerator.GenerateAccessToken(user);
        var antiforgeryToken = antiforgery.GetAndStoreTokens(context);
        await context.SignOutAsync(FlipsterAuthenticationSchemes.CookieScheme.SchemeName);
        return Results.Ok(new Register.Response(
            accessToken,
            antiforgeryToken.RequestToken,
            mapper.Map<UserDto>(user)));
    }

    [Authorize(AuthenticationSchemes = FlipsterAuthenticationSchemes.CookieScheme.SchemeName)]
    private static async Task<IResult> LoginAsync(
        HttpContext context,
        [FromServices] IAuthService authService,
        [FromServices] ITokenGenerator tokenGenerator,
        [FromServices] IFavoriteRepository favoriteRepository,
        [FromServices] ICatalogModule catalogModule,
        [FromServices] IMapper mapper,
        [FromServices] IAntiforgery antiforgery,
        [FromBody] Login.Request request)
    {
        var visitorId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = authService.Login(request.Email, request.Password);
        var refreshToken = tokenGenerator.GenerateRefreshToken();
        authService.Refresh(user, refreshToken);
        context.Response.Cookies.Append(RefreshTokenCookieName, refreshToken);
        var accessToken = tokenGenerator.GenerateAccessToken(user);
        var antiforgeryToken = antiforgery.GetAndStoreTokens(context);
        await context.SignOutAsync(FlipsterAuthenticationSchemes.CookieScheme.SchemeName);
        favoriteRepository.Update(visitorId, user.Id);
        catalogModule.UpdateViews(visitorId, user.Id);
        return Results.Ok(new Register.Response(
            accessToken,
            antiforgeryToken.RequestToken,
            mapper.Map<UserDto>(user)));
    }

    private static async Task<IResult> RefreshAsync(
        HttpContext context,
        [FromServices] ITokenGenerator tokenGenerator,
        [FromServices] ITokenRepository tokenRepository,
        [FromServices] IUserRepository userRepository,
        [FromServices] IAuthService authService,
        [FromServices] IMapper mapper)
    {
        var refreshTokenCookie = context.Request.Cookies[RefreshTokenCookieName];
        if (refreshTokenCookie == null || tokenRepository.GetByValue(refreshTokenCookie) is not Token token)
        {
            try
            {
                context.Response.Cookies.Delete(
                    context.Request.Cookies.SingleOrDefault(cookie => cookie.Key.Contains(".AspNetCore.Antiforgery.")).Key);
            }
            catch {}
            return Results.Unauthorized();
        }
        if (DateTime.UtcNow >= token.ExpiryIn)
        {
            try
            {
                context.Response.Cookies.Delete(
                    context.Request.Cookies.SingleOrDefault(cookie => cookie.Key.Contains(".AspNetCore.Antiforgery.")).Key);
            }
            catch {}
            return TypedResults.Unauthorized();
        }
        var user = userRepository.GetById(token.UserId);
        var refreshToken = tokenGenerator.GenerateRefreshToken();
        authService.Refresh(user, refreshToken);
        context.Response.Cookies.Append(RefreshTokenCookieName, refreshToken);
        var accessToken = tokenGenerator.GenerateAccessToken(user);
        return Results.Ok(new Refresh.Response(
            accessToken,
            mapper.Map<UserDto>(user)));
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    private static async Task<IResult> LogoutAsync(
        HttpContext context,
        [FromServices] IAuthService authService,
        [FromServices] ITokenGenerator tokenGenerator,
        [FromServices] IUserRepository userRepository)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = userRepository.GetById(userId);
        context.Response.Cookies.Delete(
                context.Request.Cookies.SingleOrDefault(cookie => cookie.Key.Contains(".AspNetCore.Antiforgery.")).Key);
        context.Response.Cookies.Delete(RefreshTokenCookieName);
        authService.Logout(user);
        return Results.Ok();
    }
}