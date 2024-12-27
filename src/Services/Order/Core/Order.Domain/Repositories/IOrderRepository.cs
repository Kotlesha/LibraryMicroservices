using Shared.CleanArchitecture.Domain.Repositories.Base;

namespace Order.Domain.Repositories;

using Order = Entities.Order;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order?> GetOrderByIdAsync(Guid orderId,
        CancellationToken cancellationToken = default);
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId, 
        CancellationToken cancellationToken = default);
}
