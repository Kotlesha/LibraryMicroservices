using Shared.CleanArchitecture.Common;

namespace Book.Domain.Errors;

public static partial class DomainErrors
{
    public static class Book
    {
        public static readonly Error AuthorAlreadyExists = new(
           code: "Book.AddAuthorToBook",
           message: "This author has already been added to the book.");

        public static readonly Error AuthorNotFound = new(
           code: "Book.RemoveAuthorFromBook",
           message: "There is no such author in the book.");

        public static readonly Error GenreAlreadyExists = new(
            code: "Book.AddGenreToBook",
            message: "This genre has already been added to the book.");

        public static readonly Error GenreNotFound = new(
           code: "Book.RemoveGenreFromBook",
           message: "The book does not have such a genre.");
    }
}