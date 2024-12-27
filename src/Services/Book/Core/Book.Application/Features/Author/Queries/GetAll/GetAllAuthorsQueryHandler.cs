using AutoMapper;
using Book.Application.DTOs.ResponseDTOs;
using Book.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;

namespace Book.Application.Features.Author.Queries.GetAll;

internal class GetAllAuthorsQueryHandler(
    IAuthorRepository authorRepository,
    IMapper mapper) : IQueryHandler<GetAllAuthorsQuery, IEnumerable<AuthorResponseDTO>>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<AuthorResponseDTO>> Handle(
        GetAllAuthorsQuery request, 
        CancellationToken cancellationToken)
    {
        var authors = _authorRepository.GetAll();
        return _mapper.Map<IEnumerable<AuthorResponseDTO>>(authors);
    }
}