using Microsoft.AspNetCore.Mvc;
using Shared.CleanArchitecture.Presentation.Helpers;
using Shared.Components.Errors;

namespace Shared.CleanArchitecture.Presentation.Factories;

public static class ProblemDetailsFactory
{
    public static ProblemDetails CreateProblemDetails(Error error)
    {
        var problemDetails = new ProblemDetails
        {
            Status = ProblemDetailsHelper.GetStatusCode(error.Type),
            Title = ProblemDetailsHelper.GetTitle(error.Type),
            Type = ProblemDetailsHelper.GetType(error.Type),
            Extensions = new Dictionary<string, object?>
            {
                { "errors", new[] { error } }
            }
        };

        return problemDetails;            
    }
}
