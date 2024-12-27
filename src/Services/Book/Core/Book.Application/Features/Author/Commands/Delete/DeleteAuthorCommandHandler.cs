using Book.Application.Errors;
using Book.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Components.Results;

namespace Book.Application.Features.Author.Commands.Delete;

internal class DeleteAuthorCommandHandler(
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteAuthorCommand, Result>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(request.AuthorId);

        if (author is null)
        {
            return Result.Failure(ApplicationErrors.Author.NotFound);
        }

        _authorRepository.Remove(author);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}