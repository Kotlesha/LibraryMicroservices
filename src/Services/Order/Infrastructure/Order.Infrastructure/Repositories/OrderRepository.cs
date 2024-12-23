using Microsoft.EntityFrameworkCore;
using Order.Domain.Repositories;
using Order.Infrastructure.Contexts;
using Shared.CleanArchitecture.Infrastructure.Repositories;

namespace Order.Infrastructure.Repositories;

using Order = Domain.Entities.Order;

internal class OrderRepository(OrderDbContext dbContext) : Repository<Order>(dbContext), IOrderRepository
{
    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await GetByCondition(o => o.UserId == userId)
            .Include(o => o.Books)
            .ToListAsync(cancellationToken);
    }
}
