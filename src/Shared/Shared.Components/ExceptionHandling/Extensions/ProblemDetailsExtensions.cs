using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Shared.Components.ExceptionHandling.Extensions;

public static class ProblemDetailsExtensions
{
    public static IServiceCollection AddExtendedProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Instance =
                    $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

                var existingExtensions = new Dictionary<string, object>(context.ProblemDetails.Extensions!);

                context.ProblemDetails.Extensions.Clear();
                context.ProblemDetails.Extensions["requestId"] = context.HttpContext.TraceIdentifier;

                Activity? activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                context.ProblemDetails.Extensions["traceId"] = activity?.Id;

                foreach (var kvp in existingExtensions)
                {
                    context.ProblemDetails.Extensions[kvp.Key] = kvp.Value;
                }
            };

        });

        return services;
    }

    public static ProblemDetails WithValidationErrors(this ProblemDetails problemDetails,
        IDictionary<string, string[]>? errors)
    {
        problemDetails.Extensions["errors"] = errors;
        return problemDetails;
    }
}
