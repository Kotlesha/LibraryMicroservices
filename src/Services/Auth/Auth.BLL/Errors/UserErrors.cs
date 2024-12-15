using FluentValidation.Results;
using Shared.Components.Errors;

namespace Auth.BLL.Errors;

internal static class UserErrors
{
    internal static Error InvalidPasswordOrEmail = Error.BadRequest(
        code: "Login.InvalidPasswordOrEmail",
        message: "Invalid password or email");

    internal static Error LoginValidationFailure(string errors) => Error.Validation(
        code: "Login.LoginValidationFailure",
        message: errors);

    internal static Error RegisterValidationFailure(string errors) => Error.Validation(
        code: "Login.RegisterValidationFailure",
        message: errors);
}
