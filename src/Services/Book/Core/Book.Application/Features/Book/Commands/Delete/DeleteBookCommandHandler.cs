using Book.Application.Errors;
using Book.Domain.Repositories;
using MassTransit;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Components.Results;
using Shared.Messaging.Messages.Book;

namespace Book.Application.Features.Book.Commands.Delete;

internal class DeleteBookCommandHandler(
    IBookRepository bookRepository, 
    IPublishEndpoint publishEndpoint,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteBookCommand, Result>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId);

        if (book is null)
        {
            return Result.Failure(ApplicationErrors.Book.NotFound);
        }

        _bookRepository.Remove(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _publishEndpoint.Publish(
            new BookDeletedEvent(book.Id),
            cancellationToken);

        return Result.Success();
    }
}