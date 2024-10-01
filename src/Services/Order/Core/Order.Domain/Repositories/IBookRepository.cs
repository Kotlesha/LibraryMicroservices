using Order.Domain.Entities;
using Shared.CleanArchitecture.Domain.Repositories;

namespace Order.Domain.Repositories;

using Book = Entities.Book;

public interface IBookRepository : IRepository<Book>
{
    Task<Book> GetBookByTitleAsync(Book book, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> GetBooksByPriceAsync(string price, CancellationToken cancellationToken = default);

}
