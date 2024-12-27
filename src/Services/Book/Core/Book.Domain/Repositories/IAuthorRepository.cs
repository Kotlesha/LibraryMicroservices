using Book.Domain.Entities;
using Shared.CleanArchitecture.Domain.Repositories.Base;

namespace Book.Domain.Repositories;

using Book = Entities.Book;

public interface IAuthorRepository : IRepository<Author>
{
    Task<IEnumerable<Author>?> GetAuthorBySurnameAsync(string surname,
        CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>?> GetBooksByAuthorAsync(Guid authorId, 
        CancellationToken cancellationToken = default);
    Task<Author?> GetAuthorByIdAsync(Guid authorId,
        CancellationToken cancellationToken = default);
}