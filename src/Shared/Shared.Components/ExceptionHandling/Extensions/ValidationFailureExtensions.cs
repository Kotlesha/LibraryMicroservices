using FluentValidation.Results;

namespace Shared.Components.ExceptionHandling.Extensions;

internal static class ValidationFailureExtensions
{
    internal static IDictionary<string, string[]> ToDictionary(
        this IEnumerable<ValidationFailure>? validationFailures)
    {
        if (validationFailures is null)
        {
            return new Dictionary<string, string[]>();
        }

        return validationFailures
            .GroupBy(vF => vF.PropertyName)
            .ToDictionary(
                p => p.Key, 
                p => p.Select(p => p.ErrorMessage).ToArray());
    }
}
