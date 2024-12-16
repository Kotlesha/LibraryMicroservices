using System.Text.RegularExpressions;

namespace Shared.Components.ExceptionHandling.Helpers;

internal static class IndexInfoExtractor
{
    private static readonly Regex indexRegex = new(@"IX_([^ ]+)", RegexOptions.Compiled);

    public static (string TableName, string FieldName) Extract(string sqlExceptionMessage)
    {
        var match = indexRegex.Match(sqlExceptionMessage);

        if (!match.Success)
        {
            return (string.Empty, string.Empty);
        }

        var indexName = match.Groups[1].Value;
        var parts = indexName.Split('_');

        if (parts.Length < 2)
        {
            return (string.Empty, string.Empty);
        }

        var tableName = parts[0];
        var fieldName = string.Join("_", parts.Skip(1));

        return (tableName, fieldName);
    }
}
