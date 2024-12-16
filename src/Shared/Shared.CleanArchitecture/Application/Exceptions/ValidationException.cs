using Shared.Components.Errors;

namespace Shared.CleanArchitecture.Application.Exceptions;

public class ValidationException : Exception
{
    public Error[]? Errors { get; }

    public ValidationException(Error[]? errors = null)
        : this(null, null, errors) { }

    public ValidationException(string? message, Error[]? errors = null)
        : this(message, null, errors) { }

    public ValidationException(string? message, Exception? innerException, Error[]? errors = null)
        : base(message, innerException)
    {
        Errors = errors;
    }
}
