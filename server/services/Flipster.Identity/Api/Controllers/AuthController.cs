using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Flipster.Identity.Core.Data;
using Flipster.Identity.Core.Domain.Entities;
using Flipster.Identity.Core.Domain.Enums;
using Flipster.Identity.Core.Dtos;
using Flipster.Identity.Core.Services.Interfaces;
using Flipster.Identity.Features.ChangePassword;
using Flipster.Identity.Features.ChangeUserInfo;
using Flipster.Identity.Features.Login;
using Flipster.Identity.Features.RefreshToken;
using Flipster.Identity.Features.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8604, CS8602

namespace Flipster.Identity.Api.Controllers;

[Route("[controller]")]
public class AuthController(
    ApplicationDbContext _db,
    IJwtTokenGenerator _jwtTokenGenerator,
    UserManager<User> _userManager,
    SignInManager<User> _signInManager,
    RoleManager<IdentityRole> _roleManager
) : ApiController
{
    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request)
    {
        var user = Flipster.Identity.Core.Domain.Entities.User.From(request);
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return BadRequest(new { Error = new { Message = "There was an unforeseen error during registration, please try again later." } });
        }

        if (!await _roleManager.RoleExistsAsync(UserRoles.User.ToString()))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User.ToString()));
        }
        await _userManager.AddToRoleAsync(user, UserRoles.User.ToString());

        await _signInManager.SignInAsync(user, false);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, (await _userManager.GetRolesAsync(user)).First()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var tokens = _jwtTokenGenerator.GenerateToken(user, claims);

        AddRefreshTokenToCookie(tokens.RefreshToken);
        return Ok(
            new RefreshTokenResponse(
                UserDto.From(user, (await _userManager.GetRolesAsync(user)).First()),
                tokens.AccessToken,
                tokens.RefreshToken)
        );
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return BadRequest(new { Error = new { Message = "No user with this e-mail address was found." } });
        }
        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
        if (!result.Succeeded)
        {
            return BadRequest(new { Error = new { Message = "Password mismatch." } });
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, (await _userManager.GetRolesAsync(user)).First()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var tokens = _jwtTokenGenerator.GenerateToken(user, claims);

        AddRefreshTokenToCookie(tokens.RefreshToken);
        return Ok(
            new RefreshTokenResponse(
                UserDto.From(user, (await _userManager.GetRolesAsync(user)).First()),
                tokens.AccessToken,
                tokens.RefreshToken)
        );
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh()
    {
        var refreshTokenValue = Request.Cookies["Flipster.Identity.RefreshToken"];
        if (await _db.RefreshTokens.SingleOrDefaultAsync(refreshToken => refreshToken.Value == refreshTokenValue) is not RefreshToken refreshToken)
        {
            return Unauthorized(new { Error = new { Message = "Refresh token is not found." } });
        }
        var user = await _userManager.FindByIdAsync(refreshToken.UserId);

        if (!refreshToken.IsValid())
        {
            return Unauthorized(new { Error = new { Message = "The refresh token has expired." } });
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, (await _userManager.GetRolesAsync(user)).First()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var tokens = _jwtTokenGenerator.GenerateToken(user, claims);

        AddRefreshTokenToCookie(tokens.RefreshToken);
        return Ok(
            new RefreshTokenResponse(
                UserDto.From(user, (await _userManager.GetRolesAsync(user)).First()),
                tokens.AccessToken,
                tokens.RefreshToken)
        );
    }

    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var refreshTokenValue = Request.Cookies["Flipster.Identity.RefreshToken"];
        if (await _db.RefreshTokens.SingleOrDefaultAsync(refreshToken => refreshToken.Value == refreshTokenValue) is not RefreshToken refreshToken)
        {
            return Unauthorized(new { Error = new { Message = "Refresh token is not found." } });
        }

        _db.Remove(refreshToken);
        await _db.SaveChangesAsync();

        RemoveRefreshTokenFromCookie();
        return Ok();
    }

    [HttpPut("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
    {
        var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.Password);
        if (result.Succeeded)
        {
            return BadRequest(new { Error = new { Message = result.Errors.First().Description } });
        }
        await _userManager.UpdateAsync(user);
        return Ok();
    }

    [HttpPut("change-user-info")]
    [Authorize]
    public async Task<IActionResult> ChangeUserInfo(ChangeUserInfoRequest request)
    {
        var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        user.UserName = request.Name;
        user.PhoneNumber = request.PhoneNumber;
        user.Avatar = request.Avatar;
        await _db.SaveChangesAsync();
        return Ok(user);
    }

    [HttpDelete("remove-user")]
    [Authorize]
    public async Task<IActionResult> RemoveUser()
    {
        var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            return BadRequest(new { Error = new { Message = result.Errors.First().Description } });
        }
        return Ok();
    }

    public void AddRefreshTokenToCookie(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(10)
        };
        Response.Cookies.Append("Flipster.Identity.RefreshToken", refreshToken, cookieOptions);
    }

    public void RemoveRefreshTokenFromCookie()
    {
        Response.Cookies.Delete("Flipster.Identity.RefreshToken");
    }
}