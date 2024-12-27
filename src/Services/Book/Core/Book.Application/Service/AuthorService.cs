using Book.Application.Abstractions.Service;
using Book.Domain.Entities;
using Book.Domain.Repositories;
using Shared.Components.Results;
using Book.Application.Errors;

namespace Book.Application.Service;

internal class AuthorService(IAuthorRepository authorRepository) : IAuthorService
{
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<Result<List<Author>>> GetAuthorsAsync(
        IEnumerable<Guid> authorsIds,
        CancellationToken cancellationToken)
    {
        var authors = new List<Author>();

        foreach (var authorId in authorsIds)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(authorId, cancellationToken);

            if (author is null)
            {
                return Result.Failure<List<Author>>(ApplicationErrors.Book.NotFound);
            }

            if (authors.Contains(author))
            {
                return Result.Failure<List<Author>>(ApplicationErrors.Book.SimiliarAuthorsIds);
            }

            authors.Add(author);
        }

        return Result.Success((authors));
    }
}