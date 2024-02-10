using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Flipster.Identity.Core.Data;
using Flipster.Identity.Core.Domain.Entities;
using Flipster.Identity.Core.Domain.Enums;
using Flipster.Identity.Core.Dtos;
using Flipster.Identity.Core.Services.Interfaces;
using Flipster.Identity.Features.Login;
using Flipster.Identity.Features.RefreshToken;
using Flipster.Identity.Features.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8604

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
            return BadRequest(
                new { Errors = result.Errors.Select(error => new { Code = error.Code, Description = error.Description }) }
            );
        }

        if (!(await _roleManager.RoleExistsAsync(UserRoles.User.ToString())))
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

        Response.Cookies.Append("Flipster.Identity.RefreshToken", tokens.RefreshToken);
        return Ok(
            new RegisterResponse(UserDto.From(user), tokens.AccessToken, tokens.RefreshToken)
        );
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
        if (!result.Succeeded)
        {
            return BadRequest(result);
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

        Response.Cookies.Append("Flipster.Identity.RefreshToken", tokens.RefreshToken);
        return Ok(
            new LoginResponse(UserDto.From(user), tokens.AccessToken, tokens.RefreshToken)
        );
    }

    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshTokenValue = Request.Cookies["Flipster.Identity.RefreshToken"];
        if (await _db.RefreshTokens.SingleOrDefaultAsync(refreshToken => refreshToken.Value == refreshTokenValue) is not RefreshToken refreshToken)
        {
            return BadRequest(new { Error = new { Message = "Refresh token is not found." } });
        }
        var user = await _userManager.FindByIdAsync(refreshToken.UserId);

        if (!refreshToken.IsValid())
        {
            return BadRequest(new { Error = new { Message = "The refresh token has expired." } });
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

        Response.Cookies.Append("Flipster.Identity.RefreshToken", tokens.RefreshToken);
        return Ok(
            new RefreshTokenResponse(UserDto.From(user), tokens.AccessToken, tokens.RefreshToken)
        );
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "User")]
    public IActionResult Tets()
    {
        return Ok("Ok");
    }
}