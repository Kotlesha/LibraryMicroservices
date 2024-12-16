using AutoMapper;
using Order.Application.DTOs.ResponseDTOs;
using Order.Application.Errors;
using Order.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Order.Application.Features.Book.Queries.GetByTitle;

internal class GetBookByTitleQueryHandler(
    IBookRepository bookRepository,
    IMapper mapper) : IQueryHandler<GetBookByTitleQuery, Result<BookResponseDTO>>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<BookResponseDTO>> Handle(GetBookByTitleQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBookByTitleAsync(request.title, cancellationToken);

        if (book is null)
        {
            return Result.Failure<BookResponseDTO>(ApplicationErrors.Book.NotFound);
        }

        var resultBook = _mapper.Map<BookResponseDTO>(book);
        return resultBook;
    }
}
