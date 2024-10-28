using Shared.CleanArchitecture.Common.Components;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.Application.Abstractions.Services;

internal interface IUserService
{
    Task<Result<UserDTO>> GetUserByIdAsync(Guid userId, 
        CancellationToken cancellationToken = default);
}
