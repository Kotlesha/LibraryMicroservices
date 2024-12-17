using Microsoft.AspNetCore.Http;
using Shared.CleanArchitecture.Application.Abstractions.Providers;
using System.Security.Claims;

namespace Shared.CleanArchitecture.Presentation.Providers;

public sealed class UserIdProvider(
    IHttpContextAccessor httpContextAccessor) : IUserIdProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string GetAuthUserId()
    {
        var userId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return userId ?? throw new InvalidOperationException("Can't find userId");
    }
}
