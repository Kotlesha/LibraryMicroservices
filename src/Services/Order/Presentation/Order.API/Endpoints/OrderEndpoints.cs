using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.DTOs.RequestDTOs;
using Order.Application.Features.Order.Commands.Create;
using Order.Application.Features.Order.Commands.Delete;
using Order.Application.Features.Order.Commands.Update;
using Order.Application.Features.Order.Queries.GetByUserId;
using Shared.CleanArchitecture.Extensions;

namespace Order.API.Endpoints;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("orders");

        endpoints.MapPost("/create", CreateOrder)
            .WithName(nameof(CreateOrder));

        endpoints.MapPost("/delete", DeleteOrder)
            .WithName(nameof(DeleteOrder));

        endpoints.MapPost("/update", UpdateOrder)
            .WithName(nameof(UpdateOrder));

        endpoints.MapGet("/get", GetOrdersByUserId)
            .WithName(nameof(GetOrdersByUserId));

        return app;
    }

    private static async Task<IResult> CreateOrder(
        [FromBody] OrderRequestDTO orderRequestDto,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new CreateOrderCommand(orderRequestDto);
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess ? Results.Created() : result.ToProblemDetails();
    }

    private static async Task<IResult> DeleteOrder(
        [FromBody] DeleteOrderCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess ?
            Results.Ok() :
            result.ToProblemDetails();
    }

    private static async Task<IResult> UpdateOrder(
        [FromBody] UpdateOrderCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess ?
            Results.Ok() :
            result.ToProblemDetails();
    }

    private static async Task<IResult> GetOrdersByUserId(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var order = await sender.Send(
            new GetOrdersByUserIdQuery(),
            cancellationToken);

        return order.Any() ?
            Results.Ok(order) :
            Results.NoContent();
    }
}
