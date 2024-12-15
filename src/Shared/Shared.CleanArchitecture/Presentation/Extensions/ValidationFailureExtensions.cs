using FluentValidation.Results;

namespace Shared.CleanArchitecture.Presentation.Extensions;

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
                vF => vF.Key,
                vF => vF.Select(vF => vF.ErrorMessage).ToArray());
    }
}
