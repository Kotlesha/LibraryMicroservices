using Book.Domain.Entities;
using Shared.CleanArchitecture.Domain.Repositories;

namespace Book.Domain.Repositories;

public interface IGenreRepository : IRepository<Genre>
{
    Task<Genre> GetGenreByNameAsync(string genreName, 
        CancellationToken cancellationToken = default);
}