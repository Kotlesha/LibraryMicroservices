using AutoMapper;
using Book.Application.DTOs.ResponseDTOs;
using Book.Application.Errors;
using Book.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Author.Queries.GetById;

internal class GetAuthorByIdQueryHandler(
    IAuthorRepository authorRepository,
    IMapper mapper) : IQueryHandler<GetAuthorByIdQuery, Result<AuthorResponseDTO>>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<AuthorResponseDTO>> Handle(
        GetAuthorByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(request.AuthorId);

        if (author is null)
        {
            return Result.Failure<AuthorResponseDTO>(ApplicationErrors.Author.NotFound);
        }

        var resultAuthor = _mapper.Map<AuthorResponseDTO>(author);

        return resultAuthor;
    }
}