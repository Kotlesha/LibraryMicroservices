using Shared.Components.Errors;

namespace Order.Application.Errors;

public static partial class ApplicationErrors
{
    public static class Book
    {
        public static readonly Error NotFound = Error.NotFound(
           code: "Book.NotFound",
           message: "Book doesn't exist");
      
        public static readonly Error AlreadyExists = Error.Conflict(
           code: "Book.AlreadyExists",
           message: "This book has already exist");
    }
}