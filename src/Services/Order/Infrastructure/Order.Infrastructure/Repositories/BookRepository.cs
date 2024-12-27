using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Domain.Repositories;
using Order.Infrastructure.Contexts;
using Shared.CleanArchitecture.Infrastructure.Repositories;

namespace Order.Infrastructure.Repositories;

internal class BookRepository(OrderDbContext dbContext) : Repository<Book>(dbContext), IBookRepository
{
    public async Task<Book?> GetBookByIdAsync(
        Guid bookId, 
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Books.FirstOrDefaultAsync(b => b.Id == bookId, cancellationToken);
    }

    public async Task<Book?> GetBookByTitleAsync(
        string title,
        CancellationToken cancellationToken = default)
    {
        return await GetByCondition(b => b.Title.ToLower().Contains(title.ToLower()))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
