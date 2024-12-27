using Microsoft.EntityFrameworkCore;
using Shared.Components.Pagination.Parameters;

namespace Shared.Components.Pagination.Extensions;

public static class PaginationExtensions
{
    public static async Task<(IEnumerable<T> Items, MetaData MetaData)> ApplyPagination<T>(
        this IQueryable<T> query,
        RequestParameters parameters,
        CancellationToken cancellationToken)
    {
        var count = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync(cancellationToken);

        var metaData = new MetaData
        {
            CurrentPage = parameters.PageSize,
            TotalPages = (int)Math.Ceiling(count / (double)parameters.PageNumber),
            PageSize = parameters.PageNumber,
            TotalCount = count
        };

        return (items, metaData);
    }
}
