using AutoMapper;
using Book.Application.Errors;
using Book.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Components.Results;

namespace Book.Application.Features.Author.Commands.Update;

using Author = Domain.Entities.Author;

internal class UpdateAuthorCommandHandler(
    IAuthorRepository authorRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateAuthorCommand, Result>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        UpdateAuthorCommand request, 
        CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(request.AuthorId);

        if (author is null)
        {
            return Result.Failure(ApplicationErrors.Author.NotFound);
        }

        var newAuthor = _mapper.Map<Author>(request.AuthorDTO);

        author.Update(newAuthor);

        _authorRepository.Update(author);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}