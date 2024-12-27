using AutoMapper;
using Book.Application.Errors;
using Book.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Components.Results;

namespace Book.Application.Features.Genre.Commands.Create;

using Genre = Domain.Entities.Genre;
internal class CreateGenreCommandHandler(
    IGenreRepository genreRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateGenreCommand, Result<Guid>>
{
    private readonly IGenreRepository _genreRepository = genreRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var genreNameAlreadyExists = await _genreRepository.GetGenreByNameAsync(
             request.GenreDTO.Name,
             cancellationToken);

        if (genreNameAlreadyExists is not null)
        {
            return Result.Failure<Guid>(ApplicationErrors.Category.NameAlreadyExists);
        }

        var genre = _mapper.Map<Genre>(request.GenreDTO);

        _genreRepository.Add(genre);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return genre.Id;
    }
}