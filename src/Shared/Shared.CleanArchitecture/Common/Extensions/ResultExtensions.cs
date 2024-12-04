using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.CleanArchitecture.Common.Components.Results;
using Shared.CleanArchitecture.Presentation.Factories;

namespace Shared.CleanArchitecture.Common.Extensions;

public static class ResultExtensions
{
    public static ProblemHttpResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        var problemDetails = ProblemDetailsFactory.CreateProblemDetails(result.Error);
        return TypedResults.Problem(problemDetails);
    }
}
