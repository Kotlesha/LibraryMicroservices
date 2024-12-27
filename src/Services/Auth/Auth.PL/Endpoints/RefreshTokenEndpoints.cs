using Auth.BLL.DTOs.RequestDTOs;
using Auth.BLL.Services.Interfaces;
using Auth.PL.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.CleanArchitecture.Extensions;

namespace Auth.PL.Endpoints;

public static class RefreshTokenEndpoints
{
    public static IEndpointRouteBuilder MapRefreshTokenEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("refresh-tokens");

        endpoints.MapPost("/login-with-refresh-token", LoginWithRefreshToken)
            .WithName(nameof(LoginWithRefreshToken))
            .AddEndpointFilter<ValidationEndpointFilter>();

        endpoints.MapPost("/revoke/{accountId:guid}", RevokeRefreshTokens)
            .WithName(nameof(RevokeRefreshTokens))
            .RequireAuthorization();

        return app;
    }

    private static async Task<IResult> LoginWithRefreshToken(
        [FromBody] LoginWithRefreshTokenDTO loginWithRefreshTokenDTO,
        IRefreshTokenService refreshTokenService)
    {
        var result = await refreshTokenService.LoginWithRefreshToken(loginWithRefreshTokenDTO);

        return result.IsSuccess ?
            Results.Ok(result.Value) :
            result.ToProblemDetails();
    }

    private static async Task<IResult> RevokeRefreshTokens(
        Guid accountId,
        IRefreshTokenService refreshTokenService)
    {
        var result = await refreshTokenService.RevokeAccountTokens(accountId);

        return result.IsSuccess ?
            Results.Ok() :
            result.ToProblemDetails();
    }
}
