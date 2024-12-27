using Shared.Components.Errors;

namespace Book.Application.Errors;

public static partial class ApplicationErrors
{
    public static class Category
    {
        public static readonly Error NameAlreadyExists = Error.Conflict(
           code: "Category.NameAlreadyExists",
           message: "Category with that name already exists");

        public static readonly Error NonExistentBooks = Error.NotFound(
            code: "Category.NonExistentBooks",
            message: "There are some books who doesn't exist");

        public static readonly Error SimiliarBooksIds = Error.Conflict(
            code: "Category.SimiliarBooksIds",
            message: "There are some books ids that are repeated");

        public static readonly Error NonExistentGenres = Error.NotFound(
            code: "Category.NonExistentGenres",
            message: "There are some genres who doesn't exist");

        public static readonly Error SimiliarGenresIds = Error.Conflict(
            code: "Category.SimiliarGenresIds",
            message: "There are some genre's ids that are repeated");

        public static readonly Error NotFound = Error.NotFound(
            code: "Category.NotFound",
            message: "Category with that id doesn't exist");
    }
}