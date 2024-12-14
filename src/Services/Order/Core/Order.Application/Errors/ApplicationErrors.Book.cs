using Shared.CleanArchitecture.Common;

namespace Order.Application.Errors;

public static partial class ApplicationErrors
{
    public static class Book
    {
        public static readonly Error AlreadyExistsWithThatTitle = new(
            code: "Book.AlreadyExistsWithThatTitle",
            message: "Book with this name is already exists");

        public static readonly Error NotFound = new(
           code: "Book.NotFound",
           message: "Book doesn't exist");

    }
}
