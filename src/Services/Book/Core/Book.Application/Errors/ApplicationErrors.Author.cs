using Shared.Components.Errors;

namespace Book.Application.Errors;

public static partial class ApplicationErrors
{
    public static class Author
    {
        public static readonly Error SurnameAlreadyExists = Error.Conflict(
            code: "Author.SurnameAlreadyExists",
            message: "Author with that surname already exists");

        public static readonly Error NotFound = Error.NotFound(
            code: "Author.NotFound",
            message: "Author with that id doesn't exist");
    }
}