using AutoMapper;
using Book.Application.Errors;
using Book.Domain.Repositories;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Components.Results;

namespace Book.Application.Features.Author.Commands.Create;

using Author = Domain.Entities.Author;

internal class CreateAuthorCommandHandler(
    IAuthorRepository authorRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateAuthorCommand, Result<Guid>>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = _mapper.Map<Author>(request.AuthorDTO);

        _authorRepository.Add(author);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return author.Id;
    }
}