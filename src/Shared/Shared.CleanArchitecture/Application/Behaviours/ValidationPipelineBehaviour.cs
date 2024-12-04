using FluentValidation;
using MediatR;
using Shared.CleanArchitecture.Common.Components;

namespace Shared.CleanArchitecture.Application.Behaviours;

public sealed class ValidationPipelineBehaviour<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationFailures = await Task.WhenAll(
            _validators.Select(validator => validator.ValidateAsync(context)));

        var errors = validationFailures
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => Error.Validation(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage))
            .ToArray();

        if (errors.Any())
        {
            throw new Exceptions.ValidationException(errors);
        }

        var response = await next();
        return response;
    }
}
