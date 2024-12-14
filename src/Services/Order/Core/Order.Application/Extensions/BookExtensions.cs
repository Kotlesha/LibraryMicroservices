using Order.Domain.Entities;
using Shared.CleanArchitecture.Common;
using Shared.CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Extensions;

//using Book = Domain.Entities.Book;
using Order = Domain.Entities.Order;

internal static class BookExtensions
{
    //internal static async Task<Result> AddAuthorsAndGenresToBookAsync(
    //    this Book book,
    //    IEnumerable<Guid> authorsIds,
    //    IEnumerable<Guid> genresIds,
    //    Func<IEnumerable<Guid>, Book, CancellationToken, Task<Result>> ValidateAuthorsAndAddToBookAsync,
    //    Func<IEnumerable<Guid>, Book, CancellationToken, Task<Result>> ValidateGenresAndAddToBookAsync,
    //    CancellationToken cancellationToken)
    //{
    //    var authorResult = await ValidateAuthorsAndAddToBookAsync(
    //        authorsIds, book, cancellationToken);

    //    if (authorResult.IsFailure)
    //    {
    //        return Result.Failure(authorResult.Error);
    //    }

    //    var genreResult = await ValidateGenresAndAddToBookAsync(
    //        genresIds, book, cancellationToken);

    //    if (genreResult.IsFailure)
    //    {
    //        return Result.Failure(genreResult.Error);
    //    }

    //    return Result.Success();
    //}

    internal static void UpdateRelateEntities<T>(
        this Order order,
        IEnumerable<T> currentEntities,
        IEnumerable<T> newEntities,
        Func<Order, T, Result> addEntity,
        Func<Order, T, Result> removeEntity)
        where T : AggregateRoot
    {
        var currentEntityIds = currentEntities.Select(e => e.Id).ToHashSet();
        var newEntityIdsSet = newEntities.Select(e => e.Id).ToHashSet();

        foreach (var entity in currentEntities)
        {
            if (!newEntityIdsSet.Contains(entity.Id))
            {
                removeEntity(order, entity);
            }
        }

        foreach (var entity in newEntities)
        {
            if (!currentEntityIds.Contains(entity.Id))
            {
                addEntity(order, entity);
            }
        }
    }

    internal static void UpdateAuthorsAndGenres(
        this Order order,
        IEnumerable<Book> books)
    {
        order.UpdateRelateEntities(
            order.Books,
            books,
            (order, book) => order.AddBookToOrder(book),
            (order, book) => order.RemoveBookFromOrder(book));

    }
}
