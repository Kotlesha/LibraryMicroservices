using MediatR;
using Microsoft.AspNetCore.Mvc;
using Book.Application.DTOs.RequestDTOs;
using Book.Application.Features.Genre.Commands.Create;
using Book.Application.Features.Genre.Commands.Delete;
using Book.Application.Features.Genre.Commands.Update;
using Book.Application.Features.Genre.Queries.GetAll;
using Book.Application.Features.Genre.Queries.GetById;
using Book.Application.Features.Genre.Queries.GetByName;
using Shared.CleanArchitecture.Extensions;

namespace Book.API.Endpoints;

public static class GenreEndpoints
{
    public static IEndpointRouteBuilder MapGenreEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("genres")
            .RequireAuthorization();

        endpoints.MapPost("/", CreateGenre)
            .WithName(nameof(CreateGenre));

        endpoints.MapDelete("/{genreId:guid}", DeleteGenre)
            .WithName(nameof(DeleteGenre));

        endpoints.MapPut("/", UpdateGenre)
            .WithName(nameof(UpdateGenre));

        endpoints.MapGet("/{genreId:guid}", GetGenreById)
            .WithName(nameof(GetGenreById));

        endpoints.MapGet("/", GetAllGenres)
            .WithName(nameof(GetAllGenres));

        endpoints.MapGet("/name/{genreName}", GetGenreByName)
            .WithName(nameof(GetGenreByName));

        return app;
    }

    private static async Task<IResult> CreateGenre(
        [FromBody] GenreRequestDTO genreRequestDto,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new CreateGenreCommand(genreRequestDto);
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess ? Results.Created() : result.ToProblemDetails();
    }

    private static async Task<IResult> DeleteGenre(
        [FromRoute] Guid genreId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteGenreCommand(genreId), cancellationToken);

        return result.IsSuccess ?
            Results.Ok() :
            result.ToProblemDetails();
    }

    private static async Task<IResult> UpdateGenre(
        [FromBody] UpdateGenreCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess ?
            Results.Ok() :
            result.ToProblemDetails();
    }

    private static async Task<IResult> GetGenreById(
        [FromRoute] Guid genreId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetGenreByIdQuery(genreId), cancellationToken);

        return result.IsSuccess ?
            Results.Ok(result.Value) :
            result.ToProblemDetails();
    }

    private static async Task<IResult> GetGenreByName(
        [FromRoute] string genreName,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetGenreByNameQuery(genreName), cancellationToken);

        return result.IsSuccess ?
            Results.Ok(result.Value) :
            result.ToProblemDetails();
    }

    private static async Task<IResult> GetAllGenres(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllGenresQuery(), cancellationToken);

        return result.Any() ?
            Results.Ok(result) :
            Results.NoContent();
    }
}