using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.DTOs.RequestDTOs;
using Order.Application.Features.Book.Commands.Create;
using Order.Application.Features.Book.Commands.Delete;
using Order.Application.Features.Book.Commands.Update;
using Order.Application.Features.Book.Queries.GetAll;
using Order.Application.Features.Book.Queries.GetById;
using Order.Application.Features.Book.Queries.GetByTitle;
using Shared.CleanArchitecture.Extensions;

namespace Order.API.Endpoints;

public static class BookEndpoints
{
    public static IEndpointRouteBuilder MapBookEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("books");

        endpoints.MapPost("/create", CreateBook)
            .WithName(nameof(CreateBook));

        endpoints.MapPost("/delete", DeleteBook)
            .WithName(nameof(DeleteBook));

        endpoints.MapPost("/update", UpdateBook)
            .WithName(nameof(UpdateBook));

        endpoints.MapGet("/get/{bookId:guid}", GetBookById)
            .WithName(nameof(GetBookById));

        endpoints.MapGet("/all", GetAllBooks)
            .WithName(nameof(GetAllBooks));

        endpoints.MapGet("/search", GetBookByTitle)
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
        [FromBody] DeleteBookCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

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
        Guid bookId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetBookByIdQuery(bookId), cancellationToken);

        return result.IsSuccess ?
            Results.Ok(result.Value) :
            result.ToProblemDetails();
    }

    private static async Task<IResult> GetBookByTitle(
        [FromQuery] string title,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetBookByTitleQuery(title), cancellationToken);

        return result.IsSuccess ?
            Results.Ok(result.Value) :
            result.ToProblemDetails();
    }

    private static async Task<IResult> GetAllBooks(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllBooksQuery(), cancellationToken);

        return result.Any() ?
            Results.Ok(result) :
            Results.NoContent();
    }
}
