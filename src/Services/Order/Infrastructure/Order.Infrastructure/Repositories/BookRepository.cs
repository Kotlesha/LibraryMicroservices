using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Domain.Repositories;
using Shared.CleanArchitecture.Infrastructure.Repositories;

namespace Order.Infrastructure.Repositories;

internal class BookRepository : Repository<Book>, IBookRepository
{
    public BookRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Book> GetBookByTitleAsync(
        string title,
        CancellationToken cancellationToken = default)
    {
        return await GetByCondition(b => b.Title.Equals(title, StringComparison.CurrentCultureIgnoreCase))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
