using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Application.Abstractions.Providers;
using Shared.CleanArchitecture.Common.Components.Results;
using User.Application.Abstractions.Services;
using User.Application.Errors;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.Application.Features.User.Queries.GetAuth;

internal class GetAuthUserQueryHandler(
    IUserIdProvider userIdProvider, 
    IUserService userService) : IQueryHandler<GetAuthUserQuery, Result<UserDTO>>
{
    private readonly IUserIdProvider _userIdProvider = userIdProvider;
    private readonly IUserService _userService = userService;

    public async Task<Result<UserDTO>> Handle(GetAuthUserQuery request, CancellationToken cancellationToken)
    {
        var userId = _userIdProvider.GetAuthUserId();

        if (!Guid.TryParse(userId, out Guid Id))
        {
            return Result.Failure<UserDTO>(ApplicationErrors.User.InvalidUserIdFromat);
        }

        return await _userService.GetUserByIdAsync(Id, cancellationToken);
    }
}
