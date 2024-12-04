﻿using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Common.Components.Results.Result;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.Application.Features.User.Queries.GetById;

public sealed record GetUserByIdQuery(Guid ApplicationUserId) : IQuery<Result<UserDTO>>;
