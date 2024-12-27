using Microsoft.AspNetCore.Http.Features;
using Ocelot.Configuration;
using System.Diagnostics;

namespace OcelotApiGateway.Extensions;

public static class ProblemDetailsExtensions
{
    public static IServiceCollection AddExtendedProblemDetailsWithOcelot(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                var method = context.HttpContext.Request.Method;
                context.ProblemDetails.Instance = $"{method} {context.HttpContext.Request.Path}";

                context.ProblemDetails.Extensions["requestId"] = context.HttpContext.TraceIdentifier;

                var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                context.ProblemDetails.Extensions["traceId"] = activity?.Id ?? "NoTrace";

                if (context.HttpContext.Items.TryGetValue("errors", out var customErrors))
                {
                    context.ProblemDetails.Extensions["errors"] = customErrors;
                }
            };
        });

        return services;
    }

}
