using Microsoft.EntityFrameworkCore;
using Shared.CleanArchitecture.Common.Pagination;

namespace Shared.CleanArchitecture.Common.Extensions;

public static class PaginationExtensions
{
    public static async Task<(IEnumerable<T> Items, MetaData MetaData)> ApplyPagination<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var count = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var metaData = new MetaData
        {
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize),
            PageSize = pageSize,
            TotalCount = count
        };

        return (items, metaData);
    }
}
