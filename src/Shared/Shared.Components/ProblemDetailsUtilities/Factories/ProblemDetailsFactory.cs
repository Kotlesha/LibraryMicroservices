using Microsoft.AspNetCore.Mvc;
using Shared.Components.Errors;
using Shared.Components.ProblemDetailsUtilities.Helpers;

namespace Shared.Components.ProblemDetailsUtilities.Factories;

public static class ProblemDetailsFactory
{
    public static ProblemDetails CreateProblemDetails(ErrorType errorType)
    {
        var problemDetails = new ProblemDetails
        {
            Status = ProblemDetailsHelper.GetStatusCode(errorType),
            Title = ProblemDetailsHelper.GetTitle(errorType),
            Type = ProblemDetailsHelper.GetType(errorType),
            Extensions = new Dictionary<string, object?>
            {
                { "errors", new[] { Error.Failure } }
            }
        };

        return problemDetails;
    }
}
