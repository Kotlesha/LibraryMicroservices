using Book.Domain.Entities;
using Shared.CleanArchitecture.Domain.Repositories;

namespace Book.Domain.Repositories;

using Book = Entities.Book;
public interface IBookRepository : IRepository<Book>
{
    Task<Book> GetBookByTitleAsync(string title, 
        CancellationToken cancellationToken = default);

    Task<Book> GetBookByAuthorAsync(Author author, 
        CancellationToken cancellationToken = default);

    Task<Book> GetBookByCategoryAsync(Category category, 
        CancellationToken cancellationToken = default);

    Task<Book> GetBookByGenreAsync(Genre genre, CancellationToken cancellationToken = default);

    Task<Book> GetBookByPriceAsync(decimal price, CancellationToken cancellationToken = default);

    Task<Book> GetBookByPublicationDateAsync(DateTimeOffset publicationDate, 
        CancellationToken cancellationToken = default);

    Task<Book> GetBookByIsAvailableAsync(bool isAvailable, 
        CancellationToken cancellationToken = default);

    Task<Book> GetBookByPagesAsync(short pages, CancellationToken cancellationToken = default);

    Task<Book> GetBookByAgeRatingAsync(AgeRating ageRating, 
        CancellationToken cancellationToken = default);
}
