using Book.Domain.Repositories;
using Book.Infrastructure.Contexts;
using Microsoft.Extensions.Caching.Distributed;
using Shared.CleanArchitecture.Infrastructure.Repositories;
using Shared.Components.Cashing;

namespace Book.Infrastructure.Repositories.Cashing;

using Book = Domain.Entities.Book;

internal class CashingBookRepository(
    BookDbContext dbContext, 
    IBookRepository bookRepository, 
    IDistributedCache cache) 
    : Repository<Book>(dbContext), IBookRepository
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IDistributedCache _distributedCache = cache;

    public async Task<Book?> GetBookByIdAsync(Guid bookId, CancellationToken cancellationToken = default) 
        => await _bookRepository.GetBookByIdAsync(bookId, cancellationToken);

    public async Task<Book?> GetBookByTitleAsync(string title, CancellationToken cancellationToken = default) 
        => await _bookRepository.GetBookByTitleAsync(title, cancellationToken);

    public async Task<List<Book>> GetBooksByAuthorAndGenre(
        Guid? authorId, 
        Guid? genreId, 
        CancellationToken cancellationToken = default)
    {
        var key = $"books1_{authorId}_{genreId}";

        if (_distributedCache.TryGetValue(key, out List<Book> cashedResponse))
        {
            return cashedResponse;
        }

        var books = await _bookRepository.GetBooksByAuthorAndGenre(authorId, genreId, cancellationToken);

        if (books.Count != 0)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            await _distributedCache.SetAsync(key, books, options, cancellationToken);
        }

        return books;
    }
}
