using Order.Domain.Entities;
using Shared.Components.Results;

namespace Order.Application.Services;

public interface IOrderService
{
    Task<Result<List<Book>>> ValidateAndRetrieveBooksAsync(
        IEnumerable<Guid> bookIds,
        CancellationToken cancellationToken);
}

