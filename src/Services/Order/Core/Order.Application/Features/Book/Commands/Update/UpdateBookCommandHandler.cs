using AutoMapper;
using Order.Application.Errors;
using Order.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Common;
using Shared.CleanArchitecture.Domain.Repositories;

namespace Order.Application.Features.Book.Commands.Update;

using Book = Domain.Entities.Book;

internal class UpdateBookCommandHandler(
    IBookRepository bookRepository,
    IMapper mapper,
    IUnitOfWork unitofWork) : ICommandHandler<UpdateBookCommand, Result>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitofWork;

    public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId, cancellationToken);

        if (book is null)
        {
            return Result.Failure(ApplicationErrors.Book.NotFound);
        }

        var newBook = _mapper.Map<Book>(request.BookDTO);
        book.Update(newBook);

        await _bookRepository.UpdateAsync(book, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
