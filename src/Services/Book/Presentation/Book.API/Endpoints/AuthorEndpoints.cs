using MediatR;
using Microsoft.AspNetCore.Mvc;
using Book.Application.DTOs.RequestDTOs;
using Book.Application.Features.Author.Commands.Create;
using Book.Application.Features.Author.Commands.Delete;
using Book.Application.Features.Author.Commands.Update;
using Book.Application.Features.Author.Queries.GetAll;
using Book.Application.Features.Author.Queries.GetById;
using Book.Application.Features.Author.Queries.GetBySurname;
using Shared.CleanArchitecture.Extensions;

namespace Book.API.Endpoints;

public static class AuthorEndpoints
{
    public static IEndpointRouteBuilder MapAuthorEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("authors")
            .RequireAuthorization();

        endpoints.MapPost("/", CreateAuthor)
            .WithName(nameof(CreateAuthor));

        endpoints.MapDelete("/{authorId:guid}", DeleteAuthor)
            .WithName(nameof(DeleteAuthor));

        endpoints.MapPut("/", UpdateAuthor)
            .WithName(nameof(UpdateAuthor));

        endpoints.MapGet("/{authorId:guid}", GetAuthorById)
            .WithName(nameof(GetAuthorById));

        endpoints.MapGet("/", GetAllAuthors)
            .WithName(nameof(GetAllAuthors));

        endpoints.MapGet("/surname/{authorSurname}", GetAuthorBySurname)
            .WithName(nameof(GetAuthorBySurname));

        return app;
    }

    private static async Task<IResult> CreateAuthor(
        [FromBody] AuthorRequestDTO authorRequestDto,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new CreateAuthorCommand(authorRequestDto);
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess ? Results.Created() : result.ToProblemDetails();
    }

    private static async Task<IResult> DeleteAuthor(
        [FromRoute] Guid authorId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteAuthorCommand(authorId), cancellationToken);

        return result.IsSuccess ?
            Results.Ok() :
            result.ToProblemDetails();
    }

    private static async Task<IResult> UpdateAuthor(
        [FromBody] UpdateAuthorCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess ?
            Results.Ok() :
            result.ToProblemDetails();
    }

    private static async Task<IResult> GetAuthorById(
        [FromRoute] Guid authorId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAuthorByIdQuery(authorId), cancellationToken);

        return result.IsSuccess ?
            Results.Ok(result.Value) :
            result.ToProblemDetails();
    }

    private static async Task<IResult> GetAuthorBySurname(
        [FromRoute] string authorSurname,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAuthorBySurnameQuery(authorSurname), cancellationToken);

        return result.IsSuccess ?
            Results.Ok(result.Value) :
            result.ToProblemDetails();
    }

    private static async Task<IResult> GetAllAuthors(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllAuthorsQuery(), cancellationToken);

        return result.Any() ?
            Results.Ok(result) :
            Results.NoContent();
    }
}