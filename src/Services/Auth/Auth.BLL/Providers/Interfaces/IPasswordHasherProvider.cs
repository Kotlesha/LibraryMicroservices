namespace Auth.BLL.Providers.Interfaces;

public interface IPasswordHasherProvider
{
    string GetPasswordHash(string password);
    bool VerifyPasswords(string password, string passwordHash);
}
