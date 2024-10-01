using Order.Domain.Entities;
using Shared.CleanArchitecture.Domain.Repositories;

namespace Order.Domain.Repositories;

using Order = Entities.Order;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order> GetOrderByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Order>> GetOrderByDateAsync(DateOnly Date, CancellationToken cancellationToken = default);
    Task AddBookToOrderAsync(Order order, Book book, CancellationToken cancellationToken = default);
    Task DeleteBookFromOrderAsync(Order order, Book book, CancellationToken cancellationToken = default);
}
