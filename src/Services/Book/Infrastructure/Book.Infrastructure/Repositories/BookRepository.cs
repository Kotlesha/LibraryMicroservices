using Microsoft.EntityFrameworkCore;
using Book.Domain.Repositories;
using Shared.CleanArchitecture.Infrastructure.Repositories;
using Book.Infrastructure.Contexts;

namespace Book.Infrastructure.Repositories;

using Book = Domain.Entities.Book;

internal class BookRepository(BookDbContext dbContext) 
    : Repository<Book>(dbContext), IBookRepository
{
    public async Task<Book?> GetBookByTitleAsync(
        string title,
        CancellationToken cancellationToken = default)
    {
        return await GetByCondition(b => b.Title.ToLower().Contains(title.ToLower()))
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<Book?> GetBookByIdAsync(Guid bookId, CancellationToken cancellationToken)
    {
        return dbContext.Books
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .FirstOrDefaultAsync(b => b.Id == bookId);
    }

    public async Task<List<Book>> GetBooksByAuthorAndGenre(
        Guid? authorId, 
        Guid? genreId, 
        CancellationToken cancellationToken)
    {
        var query = GetAll()
            .Include(b => b.Authors.Where(a => !authorId.HasValue || a.Id == authorId.Value))
            .Include(b => b.Genres.Where(g => !genreId.HasValue || g.Id == genreId.Value))
            .AsQueryable();

        if (authorId.HasValue)
        {
            query = query.Where(b => b.Authors.Any(a => a.Id == authorId.Value));
        }

        if (genreId.HasValue)
        {
            query = query.Where(b => b.Genres.Any(g => g.Id == genreId.Value));
        }

        var books = await query.ToListAsync(cancellationToken);
        return books;
    }
}