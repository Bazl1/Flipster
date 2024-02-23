using AutoMapper;
using Flipster.Modules.Users.Domain.Repositories;
using Flipster.Modules.Users.Domain.Services;
using Flipster.Modules.Users.Dtos;
using Flipster.Shared.Domain.Errors;
using Flipster.Shared.ImageStore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Flipster.Modules.Users.Endpoints.Users;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPut("/my/change-password", (Delegate)ChangePassword)
            .RequireAuthorization();
        builder.MapPut("/my/change-avatar", (Delegate)ChangeAvatar)
            .RequireAuthorization()
            .DisableAntiforgery();
        builder.MapPut("/my/", (Delegate)Change)
            .RequireAuthorization();
        builder.MapGet("/my", (Delegate)GetMyInfo)
            .RequireAuthorization();
        builder.MapDelete("/my", (Delegate)RemoveAccount)
            .RequireAuthorization();
        builder.MapGet("/{id}", (Delegate)GetById);
        builder.MapGet("/", (Delegate)GetAll);
        return builder;
    }

    private static async Task<IResult> GetAll(
        HttpContext context,
        [FromServices] IUserRepository userRepository,
        [FromServices] IMapper mapper)
    {
        return Results.Ok(mapper.Map<IEnumerable<UserDto>>(userRepository.GetAll()));
    }

    private static async Task<IResult> GetById(
        HttpContext context,
        [FromServices] IUserRepository userRepository,
        [FromServices] IMapper mapper,
        [FromRoute] string id)
    {
        var user = userRepository.GetById(id);
        if (user == null)
            throw new FlipsterError("User with given id is not found.");
        return Results.Ok(mapper.Map<UserDto>(user));
    }

    private static async Task<IResult> RemoveAccount(HttpContext context)
    {
        throw new NotImplementedException();
    }

    private static async Task<IResult> ChangePassword(
        HttpContext context,
        [FromServices] IUserService userService,
        [FromServices] IUserRepository userRepository,
        [FromBody] ChangePassword.Request request)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = userRepository.GetById(userId);
        userService.ChangePassword(user, request.CurrentPassword, request.NewPassword);
        userRepository.Update(user);
        return Results.Ok();
    }

    private static async Task<IResult> ChangeAvatar(
        HttpContext context,
        [FromServices] IUserRepository userRepository,
        [FromServices] IUserService userService,
        [FromServices] IImageService imageService,
        [FromForm] ChangeAvatar.Request request,
        [FromServices] IMapper mapper)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = userRepository.GetById(userId);
        if (user.Avatar != null)
            await imageService.RemoveImageAsync(user.Avatar);
        userService.Update(user, avatar: await imageService.LoadImageAsync(request.Avatar));
        userRepository.Update(user);
        return Results.Ok(mapper.Map<UserDto>(user));
    }

    private static async Task<IResult> Change(
        HttpContext context,
        [FromServices] IUserRepository userRepository,
        [FromServices] IUserService userService,
        [FromServices] IImageService imageService,
        [FromServices] IMapper mapper,
        [FromBody] Change.Request request)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = userRepository.GetById(userId);
        userService.Update(user, name: request.Name, phoneNumber: request.PhoneNumber, location: request.Location);
        userRepository.Update(user);
        return Results.Ok(mapper.Map<UserDto>(user));
    }

    private static async Task<IResult> GetMyInfo(
        HttpContext context,
        [FromServices] IUserRepository userRepository,
        [FromServices] IMapper mapper)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = userRepository.GetById(userId);
        return Results.Ok(mapper.Map<UserDto>(user));
    }
}
