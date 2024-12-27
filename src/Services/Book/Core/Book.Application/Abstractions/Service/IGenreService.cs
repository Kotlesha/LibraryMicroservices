using Book.Domain.Entities;
using Shared.Components.Results;

namespace Book.Application.Abstractions.Service;

internal interface IGenreService
{
    Task<Result<List<Genre>>> GetGenresAsync(
        IEnumerable<Guid> genresIds,
        CancellationToken cancellationToken);
}