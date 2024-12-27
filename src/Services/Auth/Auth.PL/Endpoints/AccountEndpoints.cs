using Auth.BLL.DTOs.RequestDTOs;
using Auth.BLL.Services.Interfaces;
using Auth.PL.Filters;
using Microsoft.AspNetCore.Mvc;
using Shared.CleanArchitecture.Extensions;

namespace Auth.PL.Endpoints;

public static class AccountEndpoints
{
    public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("accounts");

        endpoints.MapPost("/login", Login)
            .WithName(nameof(Login))
            .AddEndpointFilter<ValidationEndpointFilter>();

        endpoints.MapPost("/register", Register)
            .WithName(nameof(Register))
            .AddEndpointFilter<ValidationEndpointFilter>();

        endpoints.MapGet("/profile", GetAccountProfile)
            .WithName(nameof(GetAccountProfile))
            .RequireAuthorization();

        return app;
    }

    private static async Task<IResult> Login(
        [FromBody] LoginDTO loginDTO,
        IAccountService accountService)
    {
        var result = await accountService.Login(loginDTO);

        return result.IsSuccess ?
            Results.Ok(result.Value) :
            result.ToProblemDetails();
    }

    private static async Task<IResult> Register(
        [FromBody] RegisterDTO registerDTO,
        IAccountService accountService)
    {
        var result = await accountService.Register(registerDTO);

        return result.IsSuccess ?
            Results.Ok() :
            result.ToProblemDetails();
    }

    private static async Task<IResult> GetAccountProfile(
        IAccountService accountService,
                HttpContext httpContext)
    {
        var result = await accountService.GetAccountProfile();

        return result.IsSuccess ?
            Results.Ok(result.Value) :
            result.ToProblemDetails();
    }
}
