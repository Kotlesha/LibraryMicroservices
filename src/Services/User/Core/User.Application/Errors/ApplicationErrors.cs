using Shared.CleanArchitecture.Common.Components.Errors;

namespace User.Application.Errors;

public static class ApplicationErrors
{
    public static class User
    {
        public static readonly Error ApplicationUserIdAlreadyExists = new(
            code: "User.ApplicationUserIdAlreadyExists",
            message: "User with this applicationUserId already exists");

        public static readonly Error NotFound = new(
            code: "User.NotFound",
            message: "User not found");

        public static readonly Error InvalidUserIdFromat = new(
            code: "User.InvalidUserIdFromat",
            message: "Cannot convert userId to Guid. Invalid format");
    }
}
