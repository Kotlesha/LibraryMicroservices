using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.Application.Features.User.Commands.Create;
using User.Application.Features.User.Queries.GetAll;
using User.Application.Features.User.Queries.GetById;

namespace User.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController(ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var userId = await sender.Send(command);

            return Results.CreatedAtRoute(
                nameof(GetUserById),
                new { id = userId },  // Строчные буквы для соответствия маршруту
                userId
            );
        }

        [HttpGet("{id:guid}")]  // userId с маленькой буквы
        public async Task<IResult> GetUserById(Guid id)
        {
            var user = await sender.Send(new GetUserByIdQuery(id));

            return user.IsSuccess ?
                Results.Ok(user.Value) :
                Results.NotFound();
        }

        [HttpGet]
        public async Task<IResult> GetAllUsers(ISender sender)
        {
            var users = await sender.Send(new GetAllUsersQuery());

            return users.Any() ?
                Results.Ok(users) :
                Results.NoContent();
        }
    }
}
