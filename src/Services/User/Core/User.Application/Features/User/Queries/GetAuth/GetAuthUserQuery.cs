using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Common.Components.Results.Result;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.Application.Features.User.Queries.GetAuth;

public sealed record GetAuthUserQuery : IQuery<Result<UserDTO>>;
