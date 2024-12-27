using AutoMapper;
using Book.Application.DTOs.ResponseDTOs;
using Book.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;

namespace Book.Application.Features.Genre.Queries.GetAll;

internal class GetAllGenresQueryHandler(
    IGenreRepository genreRepository,
    IMapper mapper) : IQueryHandler<GetAllGenresQuery, IEnumerable<GenreResponseDTO>>
{
    private readonly IGenreRepository _genreRepository = genreRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<GenreResponseDTO>> Handle(
        GetAllGenresQuery request, 
        CancellationToken cancellationToken)
    {
        var genres = _genreRepository.GetAll();
        return _mapper.Map<IEnumerable<GenreResponseDTO>>(genres);
    }
}