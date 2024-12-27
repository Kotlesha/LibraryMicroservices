using Auth.DAL.Context;
using Auth.DAL.Models;
using Auth.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Auth.DAL.Repositories.Implementations;

internal class RefreshTokenRepository(AccountDbContext accountDbContext) : IRefreshTokenRepository
{
    private readonly AccountDbContext _accountDbContext = accountDbContext;

    public void AddRefreshToken(RefreshToken refreshToken) 
        => _accountDbContext.RefreshTokens.Add(refreshToken);

    public async Task DeleteAccountTokens(Guid accountId)
    {
        await _accountDbContext
            .RefreshTokens
            .Where(rt => rt.AccountId.Equals(accountId))
            .ExecuteDeleteAsync();
    }

    public async Task<RefreshToken?> GetRefreshTokenByTokenAsync(string token)
    {
        return await _accountDbContext.RefreshTokens
            .Include(r => r.Account)  
            .FirstOrDefaultAsync(r => r.Token.Equals(token));
    }
}
