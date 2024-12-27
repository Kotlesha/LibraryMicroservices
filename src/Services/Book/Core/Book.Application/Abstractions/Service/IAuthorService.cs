using Book.Domain.Entities;
using Shared.Components.Results;

namespace Book.Application.Abstractions.Service;

internal interface IAuthorService
{
    Task<Result<List<Author>>> GetAuthorsAsync(
        IEnumerable<Guid> authorsIds,
        CancellationToken cancellationToken);
}