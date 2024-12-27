using MediatR;
using Microsoft.AspNetCore.Mvc;
using Book.Application.DTOs.RequestDTOs;
using Book.Application.Features.Category.Commands.Create;
using Book.Application.Features.Category.Commands.Delete;
using Book.Application.Features.Category.Commands.Update;
using Book.Application.Features.Category.Queries.GetAll;
using Book.Application.Features.Category.Queries.GetById;
using Book.Application.Features.Category.Queries.GetByName;
using Shared.CleanArchitecture.Extensions;

namespace Book.API.Endpoints;

public static class CategoryEndpoints
{
    public static IEndpointRouteBuilder MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("categories")
            .RequireAuthorization();

        endpoints.MapPost("/", CreateCategory)
            .WithName(nameof(CreateCategory));

        endpoints.MapDelete("/{categoryId:guid}", DeleteCategory)
            .WithName(nameof(DeleteCategory));

        endpoints.MapPut("/", UpdateCategory)
            .WithName(nameof(UpdateCategory));

        endpoints.MapGet("/{categoryId:guid}", GetCategoryById)
            .WithName(nameof(GetCategoryById));

        endpoints.MapGet("/", GetAllCategories)
            .WithName(nameof(GetAllCategories));

        endpoints.MapGet("/name/{categoryName}", GetCategoryByName)
            .WithName(nameof(GetCategoryByName));

        return app;
    }

    private static async Task<IResult> CreateCategory(
        [FromBody] CategoryRequestDTO categoryRequestDto,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new CreateCategoryCommand(categoryRequestDto);
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess ? Results.Created() : result.ToProblemDetails();
    }

    private static async Task<IResult> DeleteCategory(
        [FromRoute] Guid categoryId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteCategoryCommand(categoryId), cancellationToken);

        return result.IsSuccess ?
            Results.Ok() :
            result.ToProblemDetails();
    }

    private static async Task<IResult> UpdateCategory(
        [FromBody] UpdateCategoryCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess ?
            Results.Ok() :
            result.ToProblemDetails();
    }

    private static async Task<IResult> GetCategoryById(
        [FromRoute] Guid categoryId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCategoryByIdQuery(categoryId), cancellationToken);

        return result.IsSuccess ?
            Results.Ok(result.Value) :
            result.ToProblemDetails();
    }

    private static async Task<IResult> GetCategoryByName(
        [FromRoute] string categoryName,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCategoryByNameQuery(categoryName), cancellationToken);

        return result.IsSuccess ?
            Results.Ok(result.Value) :
            result.ToProblemDetails();
    }

    private static async Task<IResult> GetAllCategories(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllCategoriesQuery(), cancellationToken);

        return result.Any() ?
            Results.Ok(result) :
            Results.NoContent();
    }
}