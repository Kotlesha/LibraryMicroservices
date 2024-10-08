using Shared.CleanArchitecture.Common;

namespace Book.Domain.Errors
{
    public static partial class DomainErrors
    {
        public static class Book
        {
            public static readonly Error AuthorAlreadyExists = new(
               code: "Book.AddAuthorToBook",
               message: "Этот автор уже добавлен к книге.");

            public static readonly Error AuthorNotFound = new(
               code: "Book.RemoveAuthorFromBook",
               message: "Такого автора нет в книге.");

            public static readonly Error GenreAlreadyExists = new(
                code: "Book.AddGenreToBook",
                message: "Этот жанр уже добавлен в книге.");

            public static readonly Error GenreNotFound = new(
               code: "Book.RemoveGenreFromBook",
               message: "Такого жанра у книги нет.");
        }
    }
}