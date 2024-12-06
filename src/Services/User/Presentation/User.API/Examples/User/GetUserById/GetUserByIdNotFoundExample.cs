using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using User.Application.Errors;

namespace User.API.Examples.User.GetUserById;

public class GetUserByIdNotFoundExample : IExamplesProvider<ProblemDetails>
{
    public ProblemDetails GetExamples()
    {
        return new()
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            Title = "Not Found Error",
            Status = 404,
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
