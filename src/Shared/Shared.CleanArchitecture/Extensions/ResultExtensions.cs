using Microsoft.AspNetCore.Http;
using Shared.CleanArchitecture.Presentation.Factories;
using Shared.Components.Results;

namespace Shared.CleanArchitecture.Extensions;

public static class ResultExtensions
{
    public static IResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        var problemDetails = ProblemDetailsFactory.CreateProblemDetails(result.Error);
        return TypedResults.Problem(problemDetails);
    }
}
