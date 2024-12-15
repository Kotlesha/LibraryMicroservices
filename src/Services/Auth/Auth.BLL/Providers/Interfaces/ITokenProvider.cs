namespace Auth.BLL.Providers.Interfaces;

public interface ITokenProvider
{
    string GenerateToken(Guid userId);
}
