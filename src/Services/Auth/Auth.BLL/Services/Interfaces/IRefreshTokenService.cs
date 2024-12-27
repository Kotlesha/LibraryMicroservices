using Auth.BLL.DTOs.RequestDTOs;
using Auth.BLL.DTOs.ResponseDTOs;
using Shared.Components.Results;

namespace Auth.BLL.Services.Interfaces;

public interface IRefreshTokenService
{
    Task<Result<AuthDTO>> LoginWithRefreshToken(LoginWithRefreshTokenDTO loginWithRefreshTokenDTO);
    Task<Result> RevokeAccountTokens(Guid accountId);
}
