using Book.Application.Abstractions.Service;
using Book.Domain.Repositories;
using Shared.Components.Results;
using Book.Application.Errors;

namespace Book.Application.Service;

using Genre = Domain.Entities.Genre;

internal class GenreService(IGenreRepository genreRepository) : IGenreService
{
    private readonly IGenreRepository _genreRepository = genreRepository;

    public async Task<Result<List<Genre>>> GetGenresAsync(
        IEnumerable<Guid> genresIds,
        CancellationToken cancellationToken)
    {
        var genres = new List<Genre>();

        foreach (var genreId in genresIds)
        {
            var genre = await _genreRepository.GetGenreByIdAsync(genreId, cancellationToken);

            if (genre is null)
            {
                return Result.Failure<List<Genre>>(ApplicationErrors.Book.NotFound);
            }

            if (genres.Contains(genre))
            {
                return Result.Failure<List<Genre>>(ApplicationErrors.Book.SimiliarGenresIds);
            }

            genres.Add(genre);
        }

        return Result.Success((genres));
    }
}