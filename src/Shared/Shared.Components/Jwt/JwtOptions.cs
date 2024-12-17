namespace Shared.Components.Jwt;

public class JwtOptions
{
    public string Secret { get; init; }
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public int ExpirationInMinutes { get; init; }
}
