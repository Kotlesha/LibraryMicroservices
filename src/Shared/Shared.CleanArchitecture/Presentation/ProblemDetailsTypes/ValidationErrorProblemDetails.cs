using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.CleanArchitecture.Common;
using Shared.CleanArchitecture.Presentation.ProblemDetailsTypes.Messages;

namespace Shared.CleanArchitecture.Presentation.ProblemDetailsTypes;

internal class ValidationErrorProblemDetails : ProblemDetails
{
    public ValidationErrorProblemDetails(string instance, Error[]? errors = null)
    {
        Title = "Validation Error";
        Detail = ExceptionMessages.ValidationErrorMessage;
        Instance = instance;
        Status = StatusCodes.Status422UnprocessableEntity;

        if (errors is not null) 
            Extensions.Add(nameof(errors), errors);
    }
}
