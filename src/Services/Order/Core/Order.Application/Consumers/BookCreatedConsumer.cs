using MassTransit;
using Order.Domain.Entities;
using Order.Domain.Repositories;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Messaging.Messages.Book;

namespace Order.Application.Consumers;

internal class BookCreatedConsumer(
    IBookRepository bookRepository,
    IUnitOfWork unitOfWork) : IConsumer<BookCreatedEvent>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Consume(ConsumeContext<BookCreatedEvent> context)
    {
        var book = Book.Create(
            context.Message.BookId,
            context.Message.Title,
            context.Message.Price);

        _bookRepository.Add(book);
        await _unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}
