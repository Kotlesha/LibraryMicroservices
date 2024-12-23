using Order.Application.Abstractions.Services;
using Order.Application.Errors;
using Order.Domain.Entities;
using Order.Domain.Repositories;
using Shared.Components.Results;

namespace Order.Application.Services;

internal class OrderService(IBookRepository bookRepository) : IOrderService
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task<Result<List<Book>>> ValidateAndRetrieveBooksAsync(
        IEnumerable<Guid> bookIds,
        CancellationToken cancellationToken)
    {
        var books = new List<Book>();

        foreach (var bookId in bookIds)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);

            if (book is null)
            {
                return Result.Failure<List<Book>>(ApplicationErrors.Book.NotFound);
            }

            if (!book.IsAvailable)
            {
                return Result.Failure<List<Book>>(ApplicationErrors.Order.NotAvailable);
            }

            if (books.Contains(book))
            {
                return Result.Failure<List<Book>>(ApplicationErrors.Book.AlreadyExists);
            }

            books.Add(book);
        }

        return books;
    }
}

