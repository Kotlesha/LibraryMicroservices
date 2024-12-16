using Shared.CleanArchitecture.Domain.Repositories.Base;

namespace Order.Domain.Repositories;

using Order = Entities.Order;

public interface IOrderRepository : IEntityBatchRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId, 
        CancellationToken cancellationToken = default);
}
