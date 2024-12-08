namespace User.Application.Features.User.Queries.Extensions;

using User = Domain.Entities.User;

internal static class UserSearchExtensions
{
    internal static IQueryable<User> ApplySearch(this IQueryable<User> query, string? searchTerm)
    {
        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(
                u => u.Name.Contains(searchTerm) ||
                u.Email.Contains(searchTerm));
        }

        return query;
    }
}
