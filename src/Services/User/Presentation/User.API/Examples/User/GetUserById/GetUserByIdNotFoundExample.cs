using Microsoft.AspNetCore.Mvc;
using Shared.CleanArchitecture.Common.Components.Errors;
using Shared.CleanArchitecture.Presentation.Helpers;
using Swashbuckle.AspNetCore.Filters;
using User.Application.Errors;

namespace User.API.Examples.User.GetUserById;

public class GetUserByIdNotFoundExample : IExamplesProvider<ProblemDetails>
{
    public ProblemDetails GetExamples()
    {
        var errorType = ErrorType.NotFound;

        return new()
        {
            Type = ProblemDetailsHelper.GetType(errorType),
            Title = ProblemDetailsHelper.GetTitle(errorType),
            Status = ProblemDetailsHelper.GetStatusCode(errorType),
            Instance = $"GET /users/{Guid.NewGuid()}",
            Extensions = new Dictionary<string, object?>()
            {
                ["requestId"] = "0HN8KIGTGINBL:0000000D",
                ["traceId"] = "00-2c55a4463952971f3c072f857966b6b1-8d0460b4aeba61fe-00",
                ["errors"] = ApplicationErrors.User.NotFound
            }
        };
    }
}
