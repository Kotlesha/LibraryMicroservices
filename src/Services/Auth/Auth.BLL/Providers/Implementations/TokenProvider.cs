using Auth.BLL.Providers.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Shared.Components.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Auth.BLL.Providers.Implementations;

internal class TokenProvider(IOptions<JwtOptions> options) : ITokenProvider
{
    private readonly JwtOptions _options = options.Value;

    public string GenerateToken(Guid userId)
    {
        string secretKey = _options.Secret;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString())
            ]),
            Expires = DateTime.UtcNow.AddMinutes(
                _options.ExpirationInMinutes),
            SigningCredentials = credentials,
            Audience = _options.Audience,
            Issuer = _options.Issuer
        };

        var handler = new JsonWebTokenHandler();
        return handler.CreateToken(tokenDescriptor);
    }


    public string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
}
