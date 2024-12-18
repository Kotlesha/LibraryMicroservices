using AutoMapper;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Components.Results;
using User.Domain.Repositories;

namespace User.Application.Features.User.Commands.Create;

using User = Domain.Entities.User;

internal class CreateUserCommandHandler(
    IUserRepository userRepository, 
    IUnitOfWork unitOfWork,
    IMapper mapper) : ICommandHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);

        _userRepository.AddUser(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id);
    }
}
