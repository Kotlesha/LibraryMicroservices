using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Application.Abstractions.Providers;
using Shared.Components.Results;
using User.Application.Abstractions.Services;
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
        var userId = Guid.Parse(_userIdProvider.GetAuthUserId());
        return await _userService.GetUserByIdAsync(userId, cancellationToken);
    }
}
