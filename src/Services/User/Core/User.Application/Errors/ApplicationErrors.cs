using Shared.Components.Errors;

namespace User.Application.Errors;

public static class ApplicationErrors
{
    public static class User
    {
        public static readonly Error NotFound = Error.NotFound(
            code: "User.NotFound",
            message: "User not found");
    }
}
