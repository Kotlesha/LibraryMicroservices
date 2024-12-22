namespace Shared.CleanArchitecture.Application.Abstractions.Providers;

public interface IUserIdProvider
{
    string GetAuthUserId();
}
