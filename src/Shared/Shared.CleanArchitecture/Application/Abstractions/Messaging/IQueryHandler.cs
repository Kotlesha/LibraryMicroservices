﻿using MediatR;

namespace Shared.CleanArchitecture.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery> : IRequestHandler<TQuery>
        where TQuery : IQuery;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>;