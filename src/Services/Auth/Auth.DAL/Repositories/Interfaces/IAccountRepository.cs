using Auth.DAL.Models;

namespace Auth.DAL.Repositories.Interfaces;

public interface IAccountRepository
{
    Task AddAccountAsync(Account account, 
        CancellationToken cancellationToken = default);

    Task<Account?> GetAccountByEmail(string email,
        CancellationToken cancellationToken = default); 
}
