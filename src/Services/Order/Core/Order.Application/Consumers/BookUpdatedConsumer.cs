using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Order.Application.DTOs.RequestDTOs;
using Order.Domain.Entities;
using Order.Domain.Repositories;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Messaging.Messages.Book;

namespace Order.Application.Consumers;

internal class BookUpdatedConsumer(
    ILogger<BookUpdatedConsumer> logger,
    IBookRepository bookRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IConsumer<BookUpdatedEvent>
{
    private readonly ILogger<BookUpdatedConsumer> _logger = logger;
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Consume(ConsumeContext<BookUpdatedEvent> context)
    {
        var book = await _bookRepository.GetBookByIdAsync(
            context.Message.BookId, context.CancellationToken);

        if (book is not null)
        {
            var bookDTO = new BookRequestDTO(context.Message.Title, context.Message.Price);
            var newBook = _mapper.Map<Book>(bookDTO);
            book.Update(newBook);

            _bookRepository.Update(book);
            await _unitOfWork.SaveChangesAsync(context.CancellationToken);
        }

        _logger.LogWarning("Book with Id: {BookId} not found", context.Message.BookId);
    }
}
