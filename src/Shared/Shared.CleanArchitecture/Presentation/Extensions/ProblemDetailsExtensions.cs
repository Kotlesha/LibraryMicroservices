using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.CleanArchitecture.Common.Components.Errors;
using System.Diagnostics;

namespace Shared.CleanArchitecture.Presentation.Extensions;

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

                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

                Activity? activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
            };
        });

        return services;
    }

    public static ProblemDetails WithValidationErrors(this ProblemDetails problemDetails, 
        IEnumerable<Error>? errors)
    {
        problemDetails.Extensions["errors"] = errors;
        return problemDetails;
    }
}
