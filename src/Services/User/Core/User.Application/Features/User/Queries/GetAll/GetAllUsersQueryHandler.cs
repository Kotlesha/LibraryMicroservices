using AutoMapper;
using Shared.CleanArchitecture.Application.Abstractions.Messaging;
using User.Application.Features.User.Queries.ResponseDTOs;
using User.Domain.Repositories;

namespace User.Application.Features.User.Queries.GetAll;

internal class GetAllUsersQueryHandler(
    IUserRepository userRepository, 
    IMapper mapper) : IQueryHandler<GetAllUsersQuery, IEnumerable<UserDTO>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllUsersAsync(cancellationToken);
        return _mapper.Map<IEnumerable<UserDTO>>(users);
    }
}
