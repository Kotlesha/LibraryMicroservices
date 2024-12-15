using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.Components.Errors;
using Shared.Components.ExceptionHandling.Extensions;
using Shared.Components.ExceptionHandling.Factories;

namespace Shared.Components.ExceptionHandling.Middleware;

public class GlobalExceptionHandlerMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlerMiddleware> logger,
    IProblemDetailsService problemDetailsService)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);

            var problemDetails = ex switch
            {
                ValidationException validationException =>
                    ProblemDetailsFactory
                        .CreateProblemDetails(Error.Validation())
                        .WithValidationErrors(validationException.Errors.ToDictionary()),

                _ => ProblemDetailsFactory.CreateProblemDetails(Error.Failure)
            };

            context.Response.StatusCode = problemDetails.Status
                ?? StatusCodes.Status500InternalServerError;

            await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                Exception = ex,
                HttpContext = context,
                ProblemDetails = problemDetails
            });
        }
    }
}
