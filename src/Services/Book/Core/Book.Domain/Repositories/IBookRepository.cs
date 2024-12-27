using Shared.CleanArchitecture.Domain.Repositories.Base;

namespace Book.Domain.Repositories;

using Book = Entities.Book;

public interface IBookRepository : IRepository<Book>
{
    Task<Book?> GetBookByTitleAsync(string title, 
        CancellationToken cancellationToken = default);
    Task<Book?> GetBookByIdAsync(
        Guid bookId, CancellationToken cancellationToken = default);
    Task<List<Book>> GetBooksByAuthorAndGenre(
        Guid? authorId, 
        Guid? genreId,
        CancellationToken cancellationToken = default);
}