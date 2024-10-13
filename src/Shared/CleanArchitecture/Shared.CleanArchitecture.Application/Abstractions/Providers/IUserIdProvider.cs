namespace Shared.CleanArchitecture.Application.Abstractions.Services;

public interface IUserIdProvider
{
    Guid GetAuthUserId();
}
