using Microsoft.AspNetCore.Http;
using Shared.Components.Errors;

namespace Shared.Components.ProblemDetailsUtilities.Helpers;

public static class ProblemDetailsHelper
{
    public static int GetStatusCode(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => StatusCodes.Status422UnprocessableEntity,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.BadRequest => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

    public static string GetTitle(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => "Unprocessable Entity Error",
            ErrorType.NotFound => "Not Found Error",
            ErrorType.Conflict => "Conflict Error",
            ErrorType.BadRequest => "Bad Request Error",
            _ => "Internal Server Error"
        };

    public static string GetType(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => "https://datatracker.ietf.org/doc/html/rfc4918#section-11.2",
            ErrorType.BadRequest => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            ErrorType.NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            ErrorType.Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
            _ => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
        };
}
