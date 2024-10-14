using Shared.CleanArchitecture.Common;

namespace Book.Domain.Errors;

public static partial class DomainErrors
{
    public static class Category
    {
        public static readonly Error BookAlreadyExists = new(
            code: "Category.AddBookToCategory",
            message: "This book has already been added to the category.");

        public static readonly Error BookNotFound = new(
           code: "Category.RemoveBookFromCategory",
           message: "There is no such book in this category.");

        public static readonly Error GenreAlreadyExists = new(
            code: "Category.AddGenreToCategory",
            message: "This genre has already been added to the category.");

        public static readonly Error GenreNotFound = new(
           code: "Category.RemoveGenreFromCategory",
           message: "There is no such genre in the category.");
    }
}