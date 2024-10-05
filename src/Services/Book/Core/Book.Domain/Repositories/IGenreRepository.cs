using Book.Domain.Entities;
using Shared.CleanArchitecture.Domain.Repositories;

namespace Book.Domain.Repositories;

public interface IGenreRepository : IRepository<Genre>
{
    Task<Genre> GetGenreByNameAsync(string name, CancellationToken cancellationToken = default);
}