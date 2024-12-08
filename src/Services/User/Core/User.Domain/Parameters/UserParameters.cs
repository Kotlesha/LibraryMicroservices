using Shared.CleanArchitecture.Common.Paging;

namespace User.Domain.Parameters;

public class UserParameters : RequestParameters
{
    public string? SearchTerm { get; init; }
}
