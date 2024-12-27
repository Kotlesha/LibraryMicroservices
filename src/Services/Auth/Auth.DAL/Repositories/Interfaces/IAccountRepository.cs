using Auth.DAL.Models;

namespace Auth.DAL.Repositories.Interfaces;

public interface IAccountRepository
{
    void AddAccount(Account account);

    Task<Account?> GetAccountByIdAsync(Guid accountId,
        CancellationToken cancellationToken = default);

    Task<Account?> GetAccountByEmailAsync(string email,
        CancellationToken cancellationToken = default); 
}
