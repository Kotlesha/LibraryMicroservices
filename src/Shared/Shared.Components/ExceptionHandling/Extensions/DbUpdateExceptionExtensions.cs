using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shared.Components.Errors;
using Shared.Components.ExceptionHandling.Helpers;

namespace Shared.Components.ExceptionHandling.Extensions;

internal static class DbUpdateExceptionExtensions
{
    public static bool IsUniqueIndexViolation(this DbUpdateException dbUpdateException)
    {
        if (dbUpdateException.InnerException is SqlException sqlException)
        {
            return sqlException.Number is 2601 or 2627;
        }

        return false;
    }

    public static Error CreateUniqueIndexError(this DbUpdateException dbUpdateException)
    {
        if (dbUpdateException.InnerException is SqlException sqlException)
        {
            var (tableName, fieldName) = IndexInfoExtractor.Extract(sqlException.Message);

            if (string.IsNullOrEmpty(tableName) && string.IsNullOrEmpty(fieldName))
            {
                return Error.NotUnique;
            }

            return Error.Conflict(
                code: Error.NotUnique.Code,
                message: "{tableName} with such {fieldName} already exists.");
        }

        return Error.NotUnique;
    }
}

