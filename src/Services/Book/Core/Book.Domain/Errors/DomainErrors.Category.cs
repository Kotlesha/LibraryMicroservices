using Shared.CleanArchitecture.Common;

namespace Book.Domain.Errors
{
    public static partial class DomainErrors
    {
        public static class Category
        {
            public static readonly Error BookAlreadyExists = new(
                code: "Category.AddBookToCategory",
                message: "Эта книга уже добавлена в категорию.");

            public static readonly Error BookNotFound = new(
               code: "Category.RemoveBookFromCategory",
               message: "Такой книги нет в категории.");

            public static readonly Error GenreAlreadyExists = new(
                code: "Category.AddGenreToCategory",
                message: "Этот жанр уже добавлен в категорию.");

            public static readonly Error GenreNotFound = new(
               code: "Category.RemoveGenreFromCategory",
               message: "Такого жанра нет в категории.");
        }
    }
}