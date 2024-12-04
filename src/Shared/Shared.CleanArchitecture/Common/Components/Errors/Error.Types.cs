namespace Shared.CleanArchitecture.Common.Components.Errors;

public partial class Error
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

    public static readonly Error NullValue
        = new("Error.NullValue", "The specified result value is null.", ErrorType.Failure);

    public static readonly Error Failure
        = new("Error.InternalServerError", "An unexpected error occurred.", ErrorType.Failure);

    public static Error Validation =>
        new("Error.ValidationError", "Validation errors occurred.", ErrorType.Validation);

    public static Error NotFound(string code, string message) => new(code, message, ErrorType.NotFound);

    public static Error Conflict(string code, string message) => new(code, message, ErrorType.Conflict);
}
