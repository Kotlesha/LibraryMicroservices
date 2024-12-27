using Book.Application.Errors;
using Book.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Components.Results;

namespace Book.Application.Features.Genre.Commands.Delete;

internal class DeleteGenreCommandHandler(
    IGenreRepository genreRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteGenreCommand, Result>
{
    private readonly IGenreRepository _genreRepository = genreRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await _genreRepository.GetByIdAsync(request.GenreId);

        if (genre is null)
        {
            return Result.Failure(ApplicationErrors.Genre.NotFound);
        }

        _genreRepository.Remove(genre);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}