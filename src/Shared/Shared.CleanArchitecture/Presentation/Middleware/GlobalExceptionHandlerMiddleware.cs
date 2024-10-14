using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.CleanArchitecture.Application.Exceptions;
using Shared.CleanArchitecture.Presentation.ProblemDetailsTypes;

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
            _logger.LogError(ex,
                "Exception occurred: {Message}", ex.Message);

            var instance = context.Request.Path;

            ProblemDetails problemDetails = ex is ValidationException validationException ?
                new ValidationErrorProblemDetails(instance, validationException.Errors) :
                new InternalServerErrorProblemDetails(instance);

            context.Response.StatusCode = (int)problemDetails.Status;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
