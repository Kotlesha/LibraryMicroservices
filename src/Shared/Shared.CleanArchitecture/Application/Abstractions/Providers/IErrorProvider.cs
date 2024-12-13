using Shared.CleanArchitecture.Domain.Entities;
using Shared.Components.Errors;

namespace Shared.CleanArchitecture.Application.Abstractions.Providers;

public interface IErrorProvider<T>
    where T : AggregateRoot
{
    Error GetError();
}
