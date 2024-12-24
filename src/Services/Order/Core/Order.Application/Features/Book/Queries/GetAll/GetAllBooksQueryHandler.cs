using AutoMapper;
using Order.Application.DTOs.ResponseDTOs;
using Order.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;

namespace Order.Application.Features.Book.Queries.GetAll;

internal class GetAllBooksQueryHandler(
    IBookRepository bookRepository,
    IMapper mapper) : IQueryHandler<GetAllBooksQuery, IEnumerable<BookResponseDTO>>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<BookResponseDTO>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var books = _bookRepository.GetAll();
        return _mapper.Map<IEnumerable<BookResponseDTO>>(books);
    }
}
