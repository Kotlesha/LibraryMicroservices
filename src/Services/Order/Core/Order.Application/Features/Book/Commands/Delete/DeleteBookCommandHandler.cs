using Order.Application.Errors;
using Order.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Components.Results;

namespace Order.Application.Features.Book.Commands.Delete;

internal class DeleteBookCommandHandler(
    IBookRepository bookRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteBookCommand, Result>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
       var book = await _bookRepository.GetByIdAsync(request.BookId, cancellationToken);

        if (book is null) 
        {
            return Result.Failure(ApplicationErrors.Book.NotFound);
        }

        await _bookRepository.RemoveAsync(book, cancellationToken);
        await  _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

