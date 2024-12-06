using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using User.Application.Errors;

namespace User.API.Examples.User.CreateUser;

public class CreateUserConflictResponseExample : IExamplesProvider<ProblemDetails>
{
    public ProblemDetails GetExamples()
    {
        return new()
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc4918#section-11.2",
            Title = "Conflict Error",
            Status = 409,
            Instance = $"POST /users/{Guid.NewGuid()}",
            Extensions = new Dictionary<string, object?>()
            {
                ["requestId"] = "0HN8KIGTGINBL:0000000D",
                ["traceId"] = "00-2c55a4463952971f3c072f857966b6b1-8d0460b4aeba61fe-00",
                ["errors"] = ApplicationErrors.User.ApplicationUserIdAlreadyExists
            }
        };
    }
}
