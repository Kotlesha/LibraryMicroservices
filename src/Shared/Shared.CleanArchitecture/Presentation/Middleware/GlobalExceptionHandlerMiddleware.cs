using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.CleanArchitecture.Application.Exceptions;
using Shared.CleanArchitecture.Presentation.ProblemDetailsTypes;
using Shared.CleanArchitecture.Common.Components;
using Microsoft.AspNetCore.Mvc;
using Shared.CleanArchitecture.Presentation.Factories;

namespace Shared.CleanArchitecture.Presentation.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);

                var instance = context.Request.Path;

                // Create ProblemDetails based on the exception type using the factory method
                ProblemDetails problemDetails;

                if (ex is ValidationException validationException)
                {
                    // You can pass validation errors if needed
                    problemDetails = ProblemDetailsFactory.CreateProblemDetails(
                        Error.Validation("ValidationError", "Validation errors occurred")
                    );
                    problemDetails.Extensions["errors"] = validationException.Errors;
                }
                else
                {
                    problemDetails = ProblemDetailsFactory.CreateProblemDetails(
                        Error.Failure("InternalServerError", "An unexpected error occurred")
                    );
                }

                context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
