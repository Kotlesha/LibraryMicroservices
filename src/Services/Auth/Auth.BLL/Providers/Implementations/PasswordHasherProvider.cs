using Auth.BLL.Providers.Interfaces;
using HashProvider = BCrypt.Net.BCrypt;

namespace Auth.BLL.Providers.Implementations;

public class PasswordHasherProvider : IPasswordHasherProvider
{
    public string GetPasswordHash(string password)
    {
        return HashProvider.HashPassword(password);
    }

    public bool VerifyPasswords(string password, string passwordHash)
    {
        return HashProvider.Verify(password, passwordHash);
    }
}
