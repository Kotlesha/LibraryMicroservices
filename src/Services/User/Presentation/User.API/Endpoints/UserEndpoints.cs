//using Carter;
//using Carter.OpenApi;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using User.Application.Features.User.Commands.Create;
//using User.Application.Features.User.Queries.GetAll;
//using User.Application.Features.User.Queries.GetAuth;
//using User.Application.Features.User.Queries.GetById;

//namespace User.API.Endpoints;

//public class UserEndpoints
//{
//    public UserEndpoints()
//    { 
//    }

//    public void AddRoutes(IEndpointRouteBuilder app)
//    {
//        app.MapPost("/create", CreateUser);
//        app.MapGet("/me", GetAuthUser)
//            .Produces(StatusCodes.Status200OK)
//            .Produces(StatusCodes.Status400BadRequest);

//        app.MapGet("/{userId}", GetUserById)
//            .Produces(StatusCodes.Status200OK)
//            .Produces(StatusCodes.Status404NotFound);

//        app.MapGet("/", GetAllUsers)
//            .Produces(StatusCodes.Status200OK)
//            .Produces(StatusCodes.Status204NoContent);   
//    }

//    public async Task<IResult> CreateUser([FromBody] CreateUserCommand command, ISender sender)
//    {
//        var userId = await sender.Send(command);

//        return Results.CreatedAtRoute(
//            nameof(GetUserById),
//            new { userId }, 
//            new { Id = userId } 
//        );
//    }

//    public async Task<IResult> GetAuthUser(ISender sender)
//    {
//        var user = await sender.Send(new GetAuthUserQuery());

//        return user.IsSuccess ?
//            Results.Ok(user) :
//            Results.BadRequest();
//    }

//    public async Task<IResult> GetUserById(Guid userId, ISender sender)
//    {
//        var user = await sender.Send(new GetUserByIdQuery(userId));

//        return user.IsSuccess ?
//            Results.Ok(user.Value) :
//            Results.NotFound(user.Error);
//    }

//    public async Task<IResult> GetAllUsers(ISender sender)
//    {
//        var users = await sender.Send(new GetAllUsersQuery());

//        return users.Any() ?
//            Results.Ok(users) :
//            Results.NoContent();
//    }
//}
