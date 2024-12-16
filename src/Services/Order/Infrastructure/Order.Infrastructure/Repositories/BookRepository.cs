using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Domain.Repositories;
using Shared.CleanArchitecture.Infrastructure.Repositories;

namespace Order.Infrastructure.Repositories;

internal class BookRepository(DbContext dbContext) : Repository<Book>(dbContext), IBookRepository
{
    public async Task<Book> GetBookByTitleAsync(
        string title, 
        CancellationToken cancellationToken = default)
    {
        return await GetByPredicateAsync(b  => b.Title.Equals(title, StringComparison.CurrentCultureIgnoreCase), cancellationToken)

    }

    public async Task<List<Book>> GetExistingEntitiesByIdsAsync(
        IEnumerable<Guid> ids, 
        CancellationToken cancellationToken = default)
    {
        var books = new List<Book>();

        foreach (var id in ids)
        {
            var book = await GetByIdAsync(id, cancellationToken);

            if (book is not null)
            {
                books.Add(book);
            }
        }

        return books;
    }
}
