using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Common.Components;
using User.Application.Abstractions.Services;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.Application.Features.User.Queries.GetById;

internal class GetUserByIdQueryHandler(
    IUserService userService) : IQueryHandler<GetUserByIdQuery, Result<UserDTO>>
{
    private readonly IUserService _userService = userService;

    public async Task<Result<UserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await 
            _userService
                .GetUserByIdAsync(
                    request.ApplicationUserId, 
                    cancellationToken);
    }
}
