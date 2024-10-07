using AutoMapper;
using User.Application.Features.User.Queries.ResponseDTOs;

namespace User.Application.Features.User.Queries.Profiles;

using User = Domain.Entities.User;

internal class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>(); 
    }
}
