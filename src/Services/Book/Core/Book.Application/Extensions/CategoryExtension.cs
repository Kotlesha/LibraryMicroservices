using Book.Domain.Entities;
using Shared.CleanArchitecture.Domain.Entities;
using Shared.Components.Results;

namespace Book.Application.Extensions;

using Book = Domain.Entities.Book;

internal static class CategoryExtension
{
    internal static async Task<Result> AddBooksAndGenresToCategoryAsync(
        this Category category,
        IEnumerable<Guid> booksIds,
        IEnumerable<Guid> genresIds,
        Func<IEnumerable<Guid>, Category, CancellationToken, Task<Result>> ValidateBooksAndAddToCategoryAsync,
        Func<IEnumerable<Guid>, Category, CancellationToken, Task<Result>> ValidateGenresAndAddToCategoryAsync,
        CancellationToken cancellationToken)
    {
        var bookResult = await ValidateBooksAndAddToCategoryAsync(
            booksIds, category, cancellationToken);

        if (bookResult.IsFailure)
        {
            return Result.Failure(bookResult.Error);
        }

        var genreResult = await ValidateGenresAndAddToCategoryAsync(
            genresIds, category, cancellationToken);

        if (genreResult.IsFailure)
        {
            return Result.Failure(genreResult.Error);
        }

        return Result.Success();
    }

    internal static void UpdateRelateEntities<T>(
        this Category category,
        IEnumerable<T> currentEntities,
        IEnumerable<T> newEntities,
        Func<Category, T, Result> addEntity,
        Func<Category, T, Result> removeEntity)
        where T : AggregateRoot
    {
        var currentEntityIds = currentEntities.Select(e => e.Id).ToHashSet();
        var newEntityIdsSet = newEntities.Select(e => e.Id).ToHashSet();

        foreach (var entity in currentEntities)
        {
            if (!newEntityIdsSet.Contains(entity.Id))
            {
                removeEntity(category, entity);
            }
        }

        foreach (var entity in newEntities)
        {
            if (!currentEntityIds.Contains(entity.Id))
            {
                addEntity(category, entity);
            }
        }
    }
}