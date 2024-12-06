using Shared.CleanArchitecture.Common.Components.Results;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.Application.Abstractions.Services;

internal interface IUserService
{
    Task<Result<UserDTO>> GetUserByIdAsync(Guid userId, 
        CancellationToken cancellationToken = default);

    Task<Result<UserDTO>> GetUserByEmailAsync(string email,
        CancellationToken cancellationToken = default);
}
