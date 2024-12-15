using Auth.DAL.Context;
using Auth.DAL.Models;
using Auth.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Auth.DAL.Repositories.Implementations;

internal class AccountRepository(AccountDbContext accountDbContext) : IAccountRepository
{
    private readonly AccountDbContext _accountDbContext = accountDbContext;

    public async Task AddAccountAsync(Account account, CancellationToken cancellationToken) => 
        await _accountDbContext.AddAsync(account, cancellationToken);

    public Task<Account?> GetAccountByEmail(string email, CancellationToken cancellationToken)
    {
        return _accountDbContext
            .Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(
                u => u.Email.Equals(email),
                cancellationToken); 
    }
}
