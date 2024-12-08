using Shared.CleanArchitecture.Common.Pagination.Parameters;

namespace User.Application.Features.User.Queries.RequestDTOs;

public class UserParameters : RequestParameters
{
    public string? SearchTerm { get; init; }
}
