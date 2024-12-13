using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.Application.Features.User.Queries.GetAuth;

public sealed record GetAuthUserQuery : IQuery<Result<UserDTO>>;
