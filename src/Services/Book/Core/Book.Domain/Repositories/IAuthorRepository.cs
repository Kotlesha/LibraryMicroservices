using Book.Domain.Entities;
using Shared.CleanArchitecture.Domain.Repositories;

namespace Book.Domain.Repositories;

using Book = Entities.Book;

public interface IAuthorRepository : IRepository<Author>
{
    Task<IEnumerable<Author>> GetAuthorBySurnameAsync(string surname,
        CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> GetBooksByAuthorAsync(string authorSurname, string authorName = "", 
        CancellationToken cancellationToken = default);
}