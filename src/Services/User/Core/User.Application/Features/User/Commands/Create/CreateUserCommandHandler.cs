using AutoMapper;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Common.Components.Results;
using Shared.CleanArchitecture.Domain.Repositories;
using User.Application.Abstractions.Services;
using User.Application.Errors;
using User.Domain.Repositories;

namespace User.Application.Features.User.Commands.Create;

using User = Domain.Entities.User;

internal class CreateUserCommandHandler(
    IUserRepository userRepository, 
    IUserService userService,
    IUnitOfWork unitOfWork,
    IMapper mapper) : ICommandHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserService _userService = userService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var isApplicationUserIdExist = await _userService.GetUserByIdAsync(
            request.ApplicationUserId, cancellationToken);

        if (isApplicationUserIdExist.IsSuccess)
        {
            return Result.Failure<Guid>(ApplicationErrors.User.ApplicationUserIdAlreadyExists);
        }

        var user = _mapper.Map<User>(request);

        await _userRepository.AddUserAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id);
    }
}
