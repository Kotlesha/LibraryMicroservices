using AutoMapper;
using Order.Application.DTOs.ResponseDTOs;
using Order.Application.Errors;
using Order.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Order.Application.Features.Book.Queries.GetById;

internal class GetBookByIdQueryHandler(
    IBookRepository bookRepository,
    IMapper mapper) : IQueryHandler<GetBookByIdQuery, Result<BookResponseDTO>>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<BookResponseDTO>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId);

        if (book is null)
        {
            return Result.Failure<BookResponseDTO>(ApplicationErrors.Book.NotFound);
        }

        var resultBook = _mapper.Map<BookResponseDTO>(book);
        return resultBook;
    }
}
