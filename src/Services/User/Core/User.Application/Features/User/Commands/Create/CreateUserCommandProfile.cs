using AutoMapper;

namespace User.Application.Features.User.Commands.Create;

using User = Domain.Entities.User;

internal class CreateUserCommandProfile : Profile
{
    public CreateUserCommandProfile()
    {
        CreateMap<CreateUserCommand, User>()
            .ConstructUsing(cmd => User.Create(
                cmd.Name,
                cmd.Surname,
                cmd.Patronymic,
                cmd.BirthDate,
                cmd.Email,
                cmd.ApplicationUserId));
    }
}
