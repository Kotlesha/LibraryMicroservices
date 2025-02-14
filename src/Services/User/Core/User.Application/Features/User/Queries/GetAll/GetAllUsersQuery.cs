﻿using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.Components.Pagination;
using User.Application.Features.User.Queries.RequestDTOs;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.Application.Features.User.Queries.GetAll;

public sealed record GetAllUsersQuery(
    UserParameters Parameters) : IQuery<(IEnumerable<UserDTO> users, MetaData metaData)>;
