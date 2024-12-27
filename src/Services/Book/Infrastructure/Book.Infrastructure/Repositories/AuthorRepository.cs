using Microsoft.EntityFrameworkCore;
using Book.Domain.Entities;
using Book.Domain.Repositories;
using Shared.CleanArchitecture.Infrastructure.Repositories;
using Book.Infrastructure.Contexts;

namespace Book.Infrastructure.Repositories;

using Book = Domain.Entities.Book;

internal class AuthorRepository(BookDbContext dbContext) 
    : Repository<Author>(dbContext), IAuthorRepository
{
    public async Task<IEnumerable<Author>?> GetAuthorBySurnameAsync(
        string surname,
        CancellationToken cancellationToken = default)
    {
        return await GetByCondition(a => a.Surname.ToLower().Contains(surname.ToLower()))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Book>?> GetBooksByAuthorAsync(
        Guid authorId, CancellationToken cancellationToken = default)
    {
        var author = await dbContext.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == authorId, cancellationToken);

        return author?.Books;
    }

    public async Task<Author?> GetAuthorByIdAsync(
        Guid authorId,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Authors.FirstOrDefaultAsync(a => a.Id == authorId, cancellationToken);
    }
}