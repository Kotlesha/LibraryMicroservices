using AutoMapper;
using Book.Application.DTOs.ResponseDTOs;
using Book.Application.Errors;
using Book.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;

namespace Book.Application.Features.Genre.Queries.GetByName;

internal class GetGenreByNameQueryHandler(
    IGenreRepository genreRepository,
    IMapper mapper) : IQueryHandler<GetGenreByNameQuery, Result<GenreResponseDTO>>
{
    private readonly IGenreRepository _genreRepository = genreRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<GenreResponseDTO>> Handle(
        GetGenreByNameQuery request, 
        CancellationToken cancellationToken)
    {
        var genre = await _genreRepository.GetGenreByNameAsync(request.GenreName);

        if (genre is null)
        {
            return Result.Failure<GenreResponseDTO>(ApplicationErrors.Genre.NotFound);
        }

        var resultGenre = _mapper.Map<GenreResponseDTO>(genre);

        return resultGenre;
    }
}