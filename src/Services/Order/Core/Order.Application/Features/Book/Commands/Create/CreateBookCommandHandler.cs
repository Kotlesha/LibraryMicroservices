
using AutoMapper;
using Order.Application.Errors;
using Order.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Components.Results;

namespace Order.Application.Features.Book.Commands.Create;

using Book = Domain.Entities.Book;
internal class CreateBookCommandHandler(IBookRepository bookRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork): ICommandHandler<CreateBookCommand, Result<Guid>>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = _mapper.Map<Book>(request.BookDTO);

        await _bookRepository.AddAsync(book, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return book.Id;
    }

}
