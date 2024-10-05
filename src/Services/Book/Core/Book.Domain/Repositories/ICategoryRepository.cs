using Shared.CleanArchitecture.Domain.Repositories;
using Book.Domain.Entities;

namespace Book.Domain.Repositories;

using Book = Entities.Book;
public interface ICategoryRepository : IRepository<Category>
{
    Task<Category> GetCategoryByNameAsync(string categoryName, CancellationToken cancellationToken = default);
    Task<IEnumerable<Genre>> GetCategoryByGenreAsync(string categoryName, 
        CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> GetBooksByCategoryAsync(string categoryName, 
        CancellationToken cancellationToken = default);
}