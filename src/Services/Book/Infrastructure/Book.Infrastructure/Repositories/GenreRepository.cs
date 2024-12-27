using Microsoft.EntityFrameworkCore;
using Book.Domain.Entities;
using Book.Domain.Repositories;
using Shared.CleanArchitecture.Infrastructure.Repositories;
using Book.Infrastructure.Contexts;

namespace Book.Infrastructure.Repositories;

internal class GenreRepository(BookDbContext dbContext) 
    : Repository<Genre>(dbContext), IGenreRepository
{
    public async Task<Genre?> GetGenreByNameAsync(
        string genreName,
        CancellationToken cancellationToken = default)
    {
        return await GetByCondition(g => g.Name.ToLower().Contains(genreName.ToLower()))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Genre>> GetExistingGenresByIdsAsync(
        IEnumerable<Guid> genresIds,
        CancellationToken cancellationToken = default)
    {
        return await GetByCondition(g => genresIds.Contains(g.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<Genre?> GetGenreByIdAsync(Guid genreId,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Genres.FirstOrDefaultAsync(g => g.Id == genreId, cancellationToken);
    }
}