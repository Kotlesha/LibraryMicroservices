using Microsoft.EntityFrameworkCore;
using Order.Domain.Repositories;
using Shared.CleanArchitecture.Infrastructure.Repositories;

namespace Order.Infrastructure.Repositories;

using Order = Domain.Entities.Order;

internal class OrderRepository(DbContext dbContext) : Repository<Order>(dbContext), IOrderRepository
{
    public async Task<List<Order>> GetExistingEntitiesByIdsAsync(
        IEnumerable<Guid> ids, 
        CancellationToken cancellationToken = default)
    {
        var orders = new List<Order>();

        foreach (var id in ids)
        {
            var order = await GetByIdAsync(id, cancellationToken);

            if (order is not null)
            {
                orders.Add(order);
            }
        }
        
        return orders;
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(
        Guid userId, 
        CancellationToken cancellationToken = default)
    {
        return await GetByPredicateAsync(o => o.UserId.Equals(userId), cancellationToken);
    }
}
