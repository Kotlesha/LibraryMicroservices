using Auth.DAL.Models;

namespace Auth.DAL.Repositories.Interfaces;

public interface IRefreshTokenRepository
{
    void AddRefreshToken(RefreshToken refreshToken);
    Task<RefreshToken?> GetRefreshTokenByTokenAsync(string token);
    Task DeleteAccountTokens(Guid accountId);
}
