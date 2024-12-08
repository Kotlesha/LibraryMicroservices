using AutoMapper;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using Shared.CleanArchitecture.Common.Extensions;
using Shared.CleanArchitecture.Common.Paging;
using User.Application.Features.User.Queries.ResponseDTOs;
using User.Domain.Repositories;

namespace User.Application.Features.User.Queries.GetAll;

internal class GetAllUsersQueryHandler(
    IUserRepository userRepository, 
    IMapper mapper) : IQueryHandler<GetAllUsersQuery, (IEnumerable<UserDTO> users, MetaData metaData)>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<(IEnumerable<UserDTO> users, MetaData metaData)> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _userRepository.GetAllUsers();

        if (!string.IsNullOrEmpty(request.Parameters.SearchTerm))
        {
            query = query.Where(
                u => u.Name.ToLower().Equals(request.Parameters.SearchTerm.ToLower()) || 
                u.Email.Equals(request.Parameters.SearchTerm));
        }

        var users = await query
            .ToPagedList(
                request.Parameters.PageNumber,
                request.Parameters.PageSize,
                cancellationToken);

        return (_mapper.Map<IEnumerable<UserDTO>>(users), users.MetaData);
    }
}
