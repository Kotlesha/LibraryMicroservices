using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.CleanArchitecture.Application.Exceptions;

namespace Shared.CleanArchitecture.Presentation.Middleware.Exceptions;

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

            var problemDetails = ex is ValidationException validationException ?
                ProblemDetailsFactory.ValidationError(instance, validationException.Errors) :
                ProblemDetailsFactory.InternalServerError(instance);

            context.Response.StatusCode = (int)problemDetails.Status;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
