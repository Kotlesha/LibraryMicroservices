using Book.Domain.Entities;
using Book.Domain.Repositories;
using Book.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Shared.CleanArchitecture.Infrastructure.Repositories;

namespace Book.Infrastructure.Repositories;

using Book = Domain.Entities.Book;

internal class CategoryRepository(BookDbContext dbContext) 
    : Repository<Category>(dbContext), ICategoryRepository
{
    public async Task<Category?> GetCategoryByNameAsync(string categoryName, CancellationToken cancellationToken = default)
    {
        return await GetByCondition(c => c.Name.ToLower().Contains(categoryName.ToLower()))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Book>?> GetBooksByCategoryAsync(string categoryName, CancellationToken cancellationToken = default)
    {
        var category = await GetCategoryByNameAsync(categoryName, cancellationToken);
        return category?.Books;
    }
}