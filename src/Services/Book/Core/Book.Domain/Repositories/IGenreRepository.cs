using Book.Domain.Entities;
using Shared.CleanArchitecture.Domain.Repositories.Base;

namespace Book.Domain.Repositories;

public interface IGenreRepository : IRepository<Genre>
{
    Task<Genre?> GetGenreByNameAsync(string genreName, 
        CancellationToken cancellationToken = default);
    Task<IEnumerable<Genre>> GetExistingGenresByIdsAsync(IEnumerable<Guid> genresIds,
        CancellationToken cancellationToken = default);
    Task<Genre?> GetGenreByIdAsync(Guid genreId,
        CancellationToken cancellationToken = default);
}