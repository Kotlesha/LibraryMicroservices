using Shared.Components.Errors;

namespace Auth.BLL.Errors;

internal static class UserErrors
{
    internal static Error InvalidPasswordOrEmail = Error.BadRequest(
        code: "Login.InvalidPasswordOrEmail",
        message: "Invalid password or email");

    internal static Error NotFound = Error.NotFound(
        code: "GetAccountProfile.AccountNotFound",
        message: "Account not found");
}
