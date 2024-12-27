using AutoMapper;
using Book.Application.Errors;
using Book.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Components.Results;

namespace Book.Application.Features.Genre.Commands.Update;

using Genre = Domain.Entities.Genre;

internal class UpdateGenreCommandHandler(
    IGenreRepository genreRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateGenreCommand, Result>
{
    private readonly IGenreRepository _genreRepository = genreRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        UpdateGenreCommand request, 
        CancellationToken cancellationToken)
    {
        var genre = await _genreRepository.GetByIdAsync(request.GenreId);

        if (genre is null)
        {
            return Result.Failure(ApplicationErrors.Genre.NotFound);
        }

        var newGenre = _mapper.Map<Genre>(request.GenreDTO);

        genre.Update(newGenre);

        _genreRepository.Update(genre);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}