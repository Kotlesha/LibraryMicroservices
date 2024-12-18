using Auth.BLL.DTOs.RequestDTOs;
using Auth.BLL.DTOs.ResponseDTOs;
using Auth.BLL.Errors;
using Auth.BLL.Providers.Interfaces;
using Auth.BLL.Services.Interfaces;
using Auth.DAL.Repositories.Implementations;
using Auth.DAL.Repositories.Interfaces;
using Shared.CleanArchitecture.Application.Abstractions.Providers;
using Shared.Components.Results;

namespace Auth.BLL.Services.Implementations;

public class RefreshTokenService(
    IRefreshTokenRepository refreshTokenRepository,
    ITokenProvider tokenProvider,
    IUserIdProvider userIdProvider,
    IUnitOfWork unitOfWork) : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly ITokenProvider _tokenProvider = tokenProvider;
    private readonly IUserIdProvider _userIdProvider = userIdProvider;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<AuthDTO>> LoginWithRefreshToken(LoginWithRefreshTokenDTO loginWithRefreshTokenDTO)
    {
        var refreshToken = await _refreshTokenRepository
            .GetRefreshTokenByTokenAsync(loginWithRefreshTokenDTO.RefreshToken);

        if (refreshToken is null || refreshToken.ExpiresOnUtc < DateTime.UtcNow)
        {
            return Result.Failure<AuthDTO>(RefreshTokenErrors.Expired);
        }

        string accessToken = _tokenProvider.GenerateToken(refreshToken.AccountId);

        refreshToken.Token = _tokenProvider.GenerateRefreshToken();
        refreshToken.ExpiresOnUtc = DateTime.UtcNow.AddDays(7);

        await _unitOfWork.SaveChangesAsync();
        return new AuthDTO(accessToken, refreshToken.Token);
    }

    public async Task<Result> RevokeAccountTokens(Guid accountId)
    {
        var id = Guid.Parse(_userIdProvider.GetAuthUserId());

        if (accountId != id)
        {
            return Result.Failure(RefreshTokenErrors.AccessDenied);
        }

        await _refreshTokenRepository.DeleteAccountTokens(accountId);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
