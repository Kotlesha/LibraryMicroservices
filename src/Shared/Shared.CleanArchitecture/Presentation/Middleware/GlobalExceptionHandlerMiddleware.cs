using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.CleanArchitecture.Application.Exceptions;
using Shared.CleanArchitecture.Presentation.ProblemDetailsTypes;
using Microsoft.AspNetCore.Mvc;
using Shared.CleanArchitecture.Presentation.Factories;
using Shared.CleanArchitecture.Common.Components.Errors;
using Shared.CleanArchitecture.Presentation.Extensions;

namespace Shared.CleanArchitecture.Presentation.Middleware;

public class GlobalExceptionHandlerMiddleware(
    RequestDelegate next, 
    ILogger<GlobalExceptionHandlerMiddleware> logger)
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
                        .CreateProblemDetails(Error.Validation)
                        .WithValidationErrors(validationException.Errors),

                _ => ProblemDetailsFactory.CreateProblemDetails(Error.Failure)
            };

            context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
