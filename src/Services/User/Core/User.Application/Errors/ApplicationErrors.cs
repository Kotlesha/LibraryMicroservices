using Shared.CleanArchitecture.Common.Components.Errors;

namespace User.Application.Errors;

public static class ApplicationErrors
{
    public static class User
    {
        public static readonly Error ApplicationUserIdAlreadyExists = Error.Conflict(
            code: "User.ApplicationUserIdAlreadyExists",
            message: "User with this applicationUserId already exists");

        public static readonly Error NotFound = Error.NotFound(
            code: "User.NotFound",
            message: "User not found");

        public static readonly Error InvalidUserIdFromat = Error.Validation(
            code: "User.InvalidUserIdFromat",
            message: "Cannot convert userId to Guid. Invalid format");
    }
}
