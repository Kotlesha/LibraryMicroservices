using FluentValidation.Results;
using Shared.Components.Errors;

namespace Shared.Components.ExceptionHandling.Extensions;

internal static class ValidationFailureExtensions
{
    internal static Error[] ToErrorArray(
        this IEnumerable<ValidationFailure>? validationFailures)
    {
        if (validationFailures is null)
        {
            return [];
        }

        return validationFailures
            .Select(
                vF => Error.Validation(
                    code: vF.PropertyName,
                    message: vF.ErrorMessage))
            .ToArray();
    }
}
