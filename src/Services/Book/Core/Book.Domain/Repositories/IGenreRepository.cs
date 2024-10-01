﻿using Book.Domain.Entities;
using Shared.CleanArchitecture.Domain.Repositories;

namespace Book.Domain.Repositories;

public interface IGenreRepository : IRepository<Genre, Guid>
{
    Task<Genre> GetGenreByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<Genre> GetGenreByCategoryAsync(Category category, 
        CancellationToken cancellationToken = default);
}
