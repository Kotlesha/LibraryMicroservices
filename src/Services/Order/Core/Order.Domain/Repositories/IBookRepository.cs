﻿using Order.Domain.Entities;
using Shared.CleanArchitecture.Domain.Repositories.Base;

namespace Order.Domain.Repositories;

public interface IBookRepository : IRepository<Book>
{
    Task<Book?> GetBookByIdAsync(Guid bookId, 
        CancellationToken cancellationToken = default);
    Task<Book?> GetBookByTitleAsync(string title, 
        CancellationToken cancellationToken = default);
}
