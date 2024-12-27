using AutoMapper;
using Book.Application.DTOs.ResponseDTOs;
using Book.Application.Errors;
using Book.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Book.Queries.GetByTitle;

internal class GetBookByTitleQueryHandler(
    IBookRepository bookRepository,
    IMapper mapper) : IQueryHandler<GetBookByTitleQuery, Result<BookResponseDTO>>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<BookResponseDTO>> Handle(
        GetBookByTitleQuery request,
        CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBookByTitleAsync(request.BookTitle);

        if (book is null)
        {
            return Result.Failure<BookResponseDTO>(ApplicationErrors.Book.NotFoundByTitle);
        }

        var resultBook = _mapper.Map<BookResponseDTO>(book);

        return resultBook;
    }
}
