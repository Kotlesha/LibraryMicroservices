using Auth.DAL.Context;
using Auth.DAL.Models;
using Auth.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Auth.DAL.Repositories.Implementations;

internal class AccountRepository(AccountDbContext accountDbContext) : IAccountRepository
{
    private readonly AccountDbContext _accountDbContext = accountDbContext;

    public void AddAccount(Account account) => 
        _accountDbContext.Accounts.Add(account);

    public async Task<Account?> GetAccountByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _accountDbContext
            .Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(
                u => u.Email.Equals(email),
                cancellationToken); 
    }

    public async Task<Account?> GetAccountByIdAsync(Guid accountId, CancellationToken cancellationToken = default)
    {
        return await _accountDbContext
            .Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(
                u => u.Id.Equals(accountId),
                cancellationToken);
    }
}
