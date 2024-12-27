using AutoMapper;
using Book.Application.DTOs.ResponseDTOs;
using Book.Application.Errors;
using Book.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Book.Queries.GetById;

internal class GetBookByIdQueryHandler(
    IBookRepository bookRepository,
    IMapper mapper) : IQueryHandler<GetBookByIdQuery, Result<BookResponseDTO>>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<BookResponseDTO>> Handle(
        GetBookByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBookByIdAsync(request.BookId, cancellationToken);

        if (book is null)
        {
            return Result.Failure<BookResponseDTO>(ApplicationErrors.Book.NotFound);
        }

        var resultBook = _mapper.Map<BookResponseDTO>(book);

        return resultBook;
    }
}