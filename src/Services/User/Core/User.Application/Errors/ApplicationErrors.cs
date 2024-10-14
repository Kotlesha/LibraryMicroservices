using Shared.CleanArchitecture.Common;

namespace User.Application.Errors;

public static class ApplicationErrors
{
    public static class User
    {
        public static readonly Error NotFound = new(
            code: "User.GetUserByIdQueryHandler",
            message: "User not found");

        public static readonly Error InvalidUserIdFromat = new(
            code: "User.GetAuthUserQueryHandler",
            message: "Cannot convert userId to Guid. Invalid format.");
    }
}
