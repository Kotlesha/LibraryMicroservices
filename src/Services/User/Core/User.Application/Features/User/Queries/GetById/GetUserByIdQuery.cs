using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Results;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.Application.Features.User.Queries.GetById;

public sealed record GetUserByIdQuery(Guid ApplicationUserId) : IQuery<Result<UserDTO>>;
