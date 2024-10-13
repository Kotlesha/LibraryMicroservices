using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.CleanArchitecture.Common.Components;

namespace Shared.CleanArchitecture.Presentation.Middleware.Exceptions
{
    internal static class ProblemDetailsFactory
    {
        internal static ProblemDetails CreateProblemDetails(
            string title,
            string detail,
            string instance,
            int statusCode,
            Error[]? errors = null)
        {
            var problemDetails = new ProblemDetails
            {
                Title = title,
                Detail = detail,
                Instance = instance,
                Status = statusCode
            };

            if (errors is not null)
            {
                problemDetails.Extensions.Add(nameof(errors), errors);
            }

            return problemDetails;
        }

        internal static ProblemDetails InternalServerError(string instance) => CreateProblemDetails(
                title: "Internal Server Error",
                detail: ExceptionMessages.InternalServerErrorMessage,
                instance: instance,
                statusCode: StatusCodes.Status500InternalServerError);

        internal static ProblemDetails ValidationError(
            string instance,
            Error[]? errors) => CreateProblemDetails(
                    title: "Validation error",
                    detail: ExceptionMessages.ValidationErrorMessage,
                    instance: instance,
                    statusCode: StatusCodes.Status422UnprocessableEntity,
                    errors: errors);
    }
}