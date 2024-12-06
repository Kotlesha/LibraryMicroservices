using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.CleanArchitecture.Application.Exceptions;
using Shared.CleanArchitecture.Common.Components.Errors;
using Shared.CleanArchitecture.Presentation.Extensions;
using Shared.CleanArchitecture.Presentation.Factories;

namespace Shared.CleanArchitecture.Presentation.Middleware;

public class GlobalExceptionHandlerMiddleware(
    RequestDelegate next, 
    ILogger<GlobalExceptionHandlerMiddleware> logger,
    IProblemDetailsService problemDetailsService)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger = logger;
    private readonly IProblemDetailsService _problemDetailsService = problemDetailsService;

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
                        .WithValidationErrors(validationException.Errors),

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
