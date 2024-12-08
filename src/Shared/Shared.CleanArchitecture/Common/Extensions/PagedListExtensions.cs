﻿using Microsoft.EntityFrameworkCore;
using Shared.CleanArchitecture.Common.Paging;

namespace Shared.CleanArchitecture.Common.Extensions;

public static class PagedListExtensions
{
    public static async Task<PagedList<T>> ToPagedList<T>(
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

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
