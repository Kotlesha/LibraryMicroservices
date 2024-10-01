using Book.Domain.Entities;
using Shared.CleanArchitecture.Domain.Repositories;

namespace Book.Domain.Repositories;

public interface IAuthorRepository : IRepository<Author, Guid>
{
    Task<Author> GetAuthorByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<Author> GetAuthorBySurnameAsync(string surname,
        CancellationToken cancellationToken = default);

    Task<Author> GetAuthorByBookAsync(Entities.Book book, CancellationToken cancellationToken = default);
}
