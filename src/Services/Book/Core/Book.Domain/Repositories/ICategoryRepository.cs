using Book.Domain.Entities;
using Shared.CleanArchitecture.Domain.Repositories.Base;

namespace Book.Domain.Repositories;

using Book = Entities.Book;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetCategoryByNameAsync(string categoryName, 
        CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>?> GetBooksByCategoryAsync(string categoryName, 
        CancellationToken cancellationToken = default);
}