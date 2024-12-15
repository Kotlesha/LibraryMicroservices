using FluentValidation.Results;
using Shared.Components.Errors;

namespace Shared.Components.ExceptionHandling.Extensions;

internal static class ValidationFailureExtensions
{
    internal static IEnumerable<Error> ToErrorList(
        this IEnumerable<ValidationFailure>? validationFailures)
    {
        if (validationFailures is null)
        {
            return [];
        }

        return validationFailures.Select(
            vF => Error.Validation(
                code: vF.PropertyName,
                message: vF.ErrorMessage));
    }
}
