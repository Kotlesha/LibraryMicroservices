using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Components.Errors;
using Shared.Components.ExceptionHandling.Extensions;
using Shared.Components.ProblemDetailsUtilities.Extensions;
using Shared.Components.ProblemDetailsUtilities.Factories;

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
                        .CreateProblemDetails(ErrorType.Validation)
                        .WithErrors(validationException.Errors.ToDictionary()),

                DbUpdateException dbUpdateException when dbUpdateException.IsUniqueIndexViolation() =>
                    ProblemDetailsFactory
                        .CreateProblemDetails(ErrorType.Conflict)
                        .WithErrors(dbUpdateException.CreateUniqueIndexError()),

                _ => ProblemDetailsFactory
                        .CreateProblemDetails(ErrorType.Failure)
                        .WithErrors(Error.Failure)
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
