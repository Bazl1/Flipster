﻿using System.Security.Claims;
using AutoMapper;
using Flipster.Modules.Identity.Domain.User.Repositories;
using Flipster.Modules.Identity.Domain.User.Services;
using Flipster.Modules.Identity.Dtos;
using Flipster.Modules.Identity.Dtos.ChangePassword;
using Flipster.Modules.Images.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Flipster.Modules.Identity.Endpoints;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPut("/change-password", ChangePassword).RequireAuthorization();
        builder.MapPut("/change-phone", ChangePhoneNumber).RequireAuthorization();
        builder.MapPut("/change-avatar", ChangeAvatar)
                .RequireAuthorization()
                .DisableAntiforgery();
        builder.MapPut("/change-details", ChangeDetails).RequireAuthorization();
        return builder;
    }

    private static async Task<IResult> ChangePassword(
        HttpContext context,
        IPasswordHasher passwordHasher,
        IUserRepository userRepository,
        ChangePasswordRequest request)
    {
        var user = userRepository.FindById(context.User.FindFirstValue(ClaimTypes.NameIdentifier)); 
        if (!passwordHasher.VerifyHashedPassword(user.PasswordHash, request.CurrentPassword))
            return Results.BadRequest(new ErrorDto("Password mismatch."));
        user.PasswordHash = passwordHasher.Hash(request.NewPassword);
        userRepository.Update(user);
        return Results.Ok();
    }

    private static async Task<IResult> ChangePhoneNumber(
        HttpContext context,
        IUserRepository userRepository,
        IMapper mapper,
        ChangePhoneNumberRequest request)
    {
        var user = userRepository.FindById(context.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var phoneNumber = request.PhoneNumber.Replace(" ", "");
        if (user.PhoneNumber == phoneNumber)
            return Results.Ok(mapper.Map<UserDto>(user));                    
        if (userRepository.FindByPhoneNumber(phoneNumber) is not null)
            return Results.BadRequest(new ErrorDto($"User with this phone number '{phoneNumber}' already exists."));
        user.PhoneNumber = phoneNumber;
        userRepository.Update(user);
        return Results.Ok(mapper.Map<UserDto>(user));
    }
    
    private static async Task<IResult> ChangeAvatar(
        HttpContext context,
        IUserRepository userRepository,
        IMapper mapper,
        IImageService imageService,
        [FromForm] ChangeAvatarRequest request)
    {
        var user = userRepository.FindById(context.User.FindFirstValue(ClaimTypes.NameIdentifier));
        user.Avatar = await imageService.LoadImageAsync(request.Avatar);
        userRepository.Update(user);
        return Results.Ok(mapper.Map<UserDto>(user));
    }

    private static async Task<IResult> ChangeDetails(
        HttpContext context,
        IUserRepository userRepository,
        IMapper mapper,
        ChangeDetailsRequest request)
    {
        var user = userRepository.FindById(context.User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (request.Name != null && !string.IsNullOrEmpty(request.Name))
            user.Name = request.Name;
        if (request.Location != null && !string.IsNullOrEmpty(request.Name))
            user.Location = request.Location;
        userRepository.Update(user);
        return Results.Ok(mapper.Map<UserDto>(user));
    }
}