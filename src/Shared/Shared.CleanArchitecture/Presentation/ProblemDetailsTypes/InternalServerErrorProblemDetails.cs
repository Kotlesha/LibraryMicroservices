using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.CleanArchitecture.Presentation.ProblemDetailsTypes.Messages;

namespace Shared.CleanArchitecture.Presentation.ProblemDetailsTypes;

internal class InternalServerErrorProblemDetails : ProblemDetails
{
    public InternalServerErrorProblemDetails(string instance)
    {
        Title = "Internal Server Error";
        Detail = ExceptionMessages.InternalServerErrorMessage;
        Instance = instance;
        Status = StatusCodes.Status500InternalServerError;
    }
}
