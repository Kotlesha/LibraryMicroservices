using Shared.CleanArchitecture.Domain.Repositories;
using Book.Domain.Entities;

namespace Book.Domain.Repositories;


public interface ICategoryRepository : IRepository<Category>
{
    Task<Category> GetCategoryByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<Category> GetCategoryByGenreAsync(Genre genre, 
        CancellationToken cancellationToken = default);
}
