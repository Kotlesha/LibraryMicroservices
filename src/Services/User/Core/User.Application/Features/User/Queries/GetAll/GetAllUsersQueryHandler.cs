using AutoMapper;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Common.Extensions;
using Shared.CleanArchitecture.Common.Pagination;
using User.Application.Features.User.Queries.ResponseDTOs;
using User.Application.Features.User.Queries.Extensions;
using User.Domain.Repositories;

namespace User.Application.Features.User.Queries.GetAll;

internal class GetAllUsersQueryHandler(
    IUserRepository userRepository, 
    IMapper mapper) : 
    IQueryHandler<GetAllUsersQuery, (IEnumerable<UserDTO> users, MetaData metaData)>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<(IEnumerable<UserDTO> users, MetaData metaData)> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _userRepository.GetAllUsers();

        var paginatedResult = await query
            .ApplySearch(request.Parameters.SearchTerm?.ToLower())
            .ApplyPagination(
                request.Parameters.PageNumber,
                request.Parameters.PageSize,
                cancellationToken);

        return (
            users: _mapper.Map<IEnumerable<UserDTO>>(paginatedResult.Items), 
            metaData: paginatedResult.MetaData);
    }
}
