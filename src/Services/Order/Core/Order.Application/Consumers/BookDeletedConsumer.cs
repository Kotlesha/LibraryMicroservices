using MassTransit;
using Microsoft.Extensions.Logging;
using Order.Domain.Repositories;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Messaging.Messages.Book;

namespace Order.Application.Consumers;

internal class BookDeletedConsumer(
    ILogger<BookDeletedConsumer> logger,
    IBookRepository bookRepository,
    IUnitOfWork unitOfWork) : IConsumer<BookDeletedEvent>
{
    private readonly ILogger<BookDeletedConsumer> _logger = logger;
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Consume(ConsumeContext<BookDeletedEvent> context)
    {
        var book = await _bookRepository.GetBookByIdAsync(
            context.Message.BookId, context.CancellationToken);

        if (book is not null)
        {
            _bookRepository.Remove(book);
            await _unitOfWork.SaveChangesAsync(context.CancellationToken);
            return;
        }

        _logger.LogWarning("Book with Id: {BookId} not found", context.Message.BookId);
    }
}
