using AutoMapper;
using Book.Application.DTOs.ResponseDTOs;
using Book.Domain.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;

namespace Book.Application.Features.Book.Queries.GetAll;

internal class GetAllBooksQueryHandler(
    IBookRepository bookRepository,
    IMapper mapper,
    IDistributedCache distributedCache) : IQueryHandler<GetAllBooksQuery, IEnumerable<BookResponseDTO>>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IDistributedCache _distributedCache = distributedCache;

    public async Task<IEnumerable<BookResponseDTO>> Handle(
        GetAllBooksQuery request,
        CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetBooksByAuthorAndGenre(
            request.AuthorId, request.GenreId, cancellationToken);

        return _mapper.Map<IEnumerable<BookResponseDTO>>(books);
    }
}
