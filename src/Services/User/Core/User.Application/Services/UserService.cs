using AutoMapper;
using Shared.CleanArchitecture.Common.Components;
using User.Application.Abstractions.Services;
using User.Application.Errors;
using User.Application.Features.User.Queries.ResponseDTOs;
using User.Domain.Repositories;

namespace User.Application.Services;

internal class UserService(
    IUserRepository userRepository,
    IMapper mapper) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<UserDTO>> GetUserByIdAsync(Guid userId, 
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetUserByIdAsync(userId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserDTO>(ApplicationErrors.User.NotFound);
        }

        var userDTO = _mapper.Map<UserDTO>(user);
        return Result.Success(userDTO);
    }
}
