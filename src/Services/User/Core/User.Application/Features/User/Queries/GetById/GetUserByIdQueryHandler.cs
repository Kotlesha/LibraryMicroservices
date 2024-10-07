using AutoMapper;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Common;
using User.Application.Errors;
using User.Application.Features.User.Queries.ResponseDTOs;
using User.Domain.Repositories;

namespace User.Application.Features.User.Queries.GetById;

internal class GetUserByIdQueryHandler(
    IUserRepository userRepository,
    IMapper mapper) : IQueryHandler<GetUserByIdQuery, Result<UserDTO>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<UserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(request.ApplicationUserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserDTO>(ApplicationErrors.User.NotFound);
        }

        var userDTO = _mapper.Map<UserDTO>(user);
        return Result.Success(userDTO);
    }
}
