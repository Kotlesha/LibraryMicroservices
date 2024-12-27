using Shared.Components.Errors;

namespace Auth.BLL.Errors;

internal static class RefreshTokenErrors
{
    internal static Error Expired = Error.BadRequest(
        code: "LoginWithRefreshToken.TokenExpired",
        message: "The refresh token has expired.");

    internal static Error AccessDenied = Error.BadRequest(
        code: "RevokeAccountTokens.AccessDenied",
        message: "You can't revoke refresh tokens.");
}
