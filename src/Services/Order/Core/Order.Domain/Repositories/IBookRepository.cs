using Order.Domain.Entities;
using Shared.CleanArchitecture.Domain.Repositories;

namespace Order.Domain.Repositories;

public interface IBookRepository : IRepository<Book>
{
    Task<Book> GetBookByTitleAsync(string title, 
        CancellationToken cancellationToken = default);
}
