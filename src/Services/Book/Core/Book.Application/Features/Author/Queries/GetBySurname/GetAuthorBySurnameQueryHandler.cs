using AutoMapper;
using Book.Application.DTOs.ResponseDTOs;
using Book.Application.Errors;
using Book.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Author.Queries.GetBySurname;

internal class GetAuthorBySurnameQueryHandler(
    IAuthorRepository authorRepository,
    IMapper mapper) : IQueryHandler<GetAuthorBySurnameQuery, Result<IEnumerable<AuthorResponseDTO>>>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<IEnumerable<AuthorResponseDTO>>> Handle(
        GetAuthorBySurnameQuery request, 
        CancellationToken cancellationToken)
    {
        var authors = await _authorRepository.GetAuthorBySurnameAsync(request.AuthorSurname, cancellationToken);

        if (!authors.Any())
        {
            return Result.Failure<IEnumerable<AuthorResponseDTO>>(ApplicationErrors.Author.NotFound);
        }

        var result = _mapper.Map<IEnumerable<AuthorResponseDTO>>(authors);
        return Result.Success(result);
    }
}