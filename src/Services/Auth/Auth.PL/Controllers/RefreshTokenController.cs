using Auth.BLL.DTOs.RequestDTOs;
using Auth.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.CleanArchitecture.Extensions;

namespace Auth.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RefreshTokenController(IRefreshTokenService refreshTokenService) : ControllerBase
{
    private readonly IRefreshTokenService _refreshTokenService = refreshTokenService;

    [HttpPost("/login-with-refresh-token")]
    public async Task<IResult> LoginWithRefreshToken(LoginWithRefreshTokenDTO loginWithRefreshTokenDTO)
    {
        var result = await _refreshTokenService.LoginWithRefreshToken(loginWithRefreshTokenDTO);

        return result.IsSuccess ?
            Results.Ok(result.Value) :
            result.ToProblemDetails();
    }

    [Authorize]
    [HttpPost("/revoke/{accountId:guid}")]
    public async Task<IResult> RevokeRefreshTokens(Guid accountId)
    {
        var result = await _refreshTokenService.RevokeAccountTokens(accountId);
        return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
    }
}
