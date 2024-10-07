using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.Application.Features.User.Queries.GetAll;

public sealed record GetAllUsersQuery : IQuery<IEnumerable<UserDTO>>;
