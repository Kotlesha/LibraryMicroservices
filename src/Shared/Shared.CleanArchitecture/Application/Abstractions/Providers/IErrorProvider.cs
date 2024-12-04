using Shared.CleanArchitecture.Common.Components.Errors;
using Shared.CleanArchitecture.Domain.Entities;

namespace Shared.CleanArchitecture.Application.Abstractions.Providers;

public interface IErrorProvider<T>
    where T : AggregateRoot
{
    Error GetError();
}
