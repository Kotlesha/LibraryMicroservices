using Order.Domain.Entities;
using Shared.CleanArchitecture.Domain.Repositories;

namespace Order.Domain.Repositories;

using Order = Entities.Order;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId, 
        CancellationToken cancellationToken = default);
    Task<IEnumerable<Order>> GetOrderByDateAsync(DateOnly Date, 
        CancellationToken cancellationToken = default);

}
