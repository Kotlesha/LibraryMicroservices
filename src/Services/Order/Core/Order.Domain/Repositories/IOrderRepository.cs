namespace Order.Domain.Repositories;

using Order.Domain.Entities;
using Shared.CleanArchitecture.Domain.Repositories;

using Order = Entities.Order;

internal interface IOrderRepository : IRepository<Order>
{
    Task<Order> GetOrderByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Order>> GetOrderByDateAsync(DateOnly Date, CancellationToken cancellationToken = default);

    Task<Order> AddBookToOrderAsync(Order order, string title, CancellationToken cancellationToken = default);

    Task<Order> DeleteBookFromOrderAsync(Order order, string title, CancellationToken cancellationToken = default);
}
