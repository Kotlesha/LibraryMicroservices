using Book.Application.DTOs.RequestDTOs;
using Book.Application.Features.Book.Commands.Create;
using Book.Application.Features.Book.Commands.Delete;
using Book.Application.Features.Book.Commands.Update;
using Book.Application.Features.Book.Queries.GetAll;
using Book.Application.Features.Book.Queries.GetById;
using Book.Application.Features.Book.Queries.GetByTitle;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.CleanArchitecture.Extensions;

namespace Book.API.Endpoints;

public static class BookEndpoints
{
    public static IEndpointRouteBuilder MapBookEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("books")
            .RequireAuthorization();

        endpoints.MapPost("/", CreateBook)
            .WithName(nameof(CreateBook));

        endpoints.MapDelete("/{bookId:guid}", DeleteBook)
            .WithName(nameof(DeleteBook));

        endpoints.MapPut("/", UpdateBook)
            .WithName(nameof(UpdateBook));

        endpoints.MapGet("/{bookId:guid}", GetBookById)
            .WithName(nameof(GetBookById));

        endpoints.MapGet("/", GetAllBooks)
            .WithName(nameof(GetAllBooks));

        endpoints.MapGet("/{title}", GetBookByTitle)
            .WithName(nameof(GetBookByTitle));

        return app;
    }

    private static async Task<IResult> CreateBook(
        [FromBody] BookRequestDTO bookRequestDto,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new CreateBookCommand(bookRequestDto);
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess ? Results.Created() : result.ToProblemDetails();
    }

    private static async Task<IResult> DeleteBook(
        [FromRoute] Guid bookId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteBookCommand(bookId), cancellationToken);

        return result.IsSuccess ?
            Results.Ok() :
            result.ToProblemDetails();
    }

    private static async Task<IResult> UpdateBook(
        [FromBody] UpdateBookCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess ?
            Results.Ok() :
            result.ToProblemDetails();
    }

    private static async Task<IResult> GetBookById(
        [FromRoute] Guid bookId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetBookByIdQuery(bookId), cancellationToken);

        return result.IsSuccess ?
            Results.Ok(result.Value) :
            result.ToProblemDetails();
    }

    private static async Task<IResult> GetBookByTitle(
        [FromRoute] string title,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetBookByTitleQuery(title), cancellationToken);

        return result.IsSuccess ?
            Results.Ok(result.Value) :
            result.ToProblemDetails();
    }

    private static async Task<IResult> GetAllBooks(
        [FromQuery] Guid? authorId,
        [FromQuery] Guid? genreId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllBooksQuery(authorId, genreId), cancellationToken);

        return result.Any() ?
            Results.Ok(result) :
            Results.NoContent();
    }
}