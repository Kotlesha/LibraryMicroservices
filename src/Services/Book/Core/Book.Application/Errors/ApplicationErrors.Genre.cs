using Shared.Components.Errors;

namespace Book.Application.Errors;

public static partial class ApplicationErrors
{
    public static class Genre
    {
        public static readonly Error NameAlreadyExists = Error.Conflict(
          code: "Genre.NameAlreadyExists",
          message: "Genre with that name already exists");

        public static readonly Error NotFound = Error.NotFound(
            code: "Genre.NotFound",
            message: "Genre with that id doesn't exist");
    }
}