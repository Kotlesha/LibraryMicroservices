using MediatR;

namespace Shared.CleanArchitecture.Application.Abstractions.Messaging;

public interface IQuery : IRequest;

public interface IQuery<out TResponse> : IRequest<TResponse>;
