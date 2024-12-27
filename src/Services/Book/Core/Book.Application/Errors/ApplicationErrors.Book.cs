using Shared.Components.Errors;

namespace Book.Application.Errors;

public static partial class ApplicationErrors
{
    public static class Book
    {
        public static readonly Error NameAlreadyExists = Error.Conflict(
            code: "Book.NameAlreadyExists",
            message: "Book with this name is already exests.");

        public static readonly Error NonExistentAuthors = Error.NotFound(
             code: "Book.NonExistentAuthors",
             message: "There are some authors who doesn't exist");

        public static readonly Error SimiliarAuthorsIds = Error.Conflict(
            code: "Book.SimiliarAuthorsIds",
            message: "There are some author's ids that are repeated");

        public static readonly Error NonExistentGenres = Error.NotFound(
             code: "Book.NonExistentGenres",
             message: "There are some genres who doesn't exist");

        public static readonly Error SimiliarGenresIds = Error.Conflict(
            code: "Book.SimiliarGenresIds",
            message: "There are some genre's ids that are repeated");

        public static readonly Error NotFound = Error.NotFound(
            code: "Book.NotFound",
             message: "Book with this id doesn't exist");

        public static readonly Error NotFoundByAuthor = Error.NotFound(
            code: "Book.NotFound",
            message: "Book with this author doesn't exist");

        public static readonly Error NotFoundByCategory = Error.NotFound(
            code: "Book.NotFound",
            message: "Book with this category doesn't exist");

        public static readonly Error NotFoundByGenre = Error.NotFound(
            code: "Book.NotFound",
            message: "Book with this genre doesn't exist");

        public static readonly Error NotFoundByTitle = Error.NotFound(
            code: "Book.NotFound",
            message: "Book with this title doesn't exist");

        public static readonly Error NotFoundCategory = Error.NotFound(
            code: "Book.NotFoundCategory",
            message: "Category with this id doesn't exist");
    }
}