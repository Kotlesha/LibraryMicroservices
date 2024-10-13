using Shared.CleanArchitecture.Domain.Repositories;

namespace Book.Domain.Repositories;

using Book = Entities.Book;

public interface IBookRepository : IRepository<Book>
{
    Task<Book> GetBookByTitleAsync(string title, 
        CancellationToken cancellationToken = default);
    Task<Book> GetBookByIsbnAsync(string isbn, 
        CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> GetBooksByCategoryAsync(string categoryName, 
        CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> GetBooksByGenreAsync(string genreName, 
        CancellationToken cancellationToken = default);
}