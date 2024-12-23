using Microsoft.AspNetCore.Mvc;

namespace Order.API.Endpoints
{
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

            endpoints.MapGet("{applicationOrderId:guid}", GetOrderByUserId)
           .WithName(nameof(GetOrderByUserId));

            return app;

        }

        private static async Task<IResult> CreateOrder(
            [FromBody] CreateOrderCommand command,
            ISender sender,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return Results.CreatedAtRoute(
                //routeName: nameof(GetUserById),
                routeValues: new { applicationOrderId = result.Value },
                result.Value);
            }

            return result.ToProblemDetails();
        }

        private static async Task<IResult> DeleteOrder(
            [FromBody] DeleteOrderCommand command,
            ISender sender,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return Results.CreatedAtRoute(
                //routeName: nameof(GetUserById),
                routeValues: new { applicationOrderId = result.Value },
                result.Value);
            }

            return result.ToProblemDetails();
        }

        private static async Task<IResult> UpdateOrder(
            [FromBody] UpdateOrderCommand command,
            ISender sender,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return Results.CreatedAtRoute(
                //routeName: nameof(GetUserById),
                routeValues: new { applicationOrderId = result.Value },
                result.Value);
            }

            return result.ToProblemDetails();
        }

        private static async Task<IResult> GetOrderByUserId(
        Guid applicationOrderId,
        ISender sender,
        CancellationToken cancellationToken)
        {
            var result = await sender.Send(
                new GetOrderByUserIdQuery(applicationOrderId),
                cancellationToken);

            return result.IsSuccess ?
                Results.Ok(result.Value) :
                result.ToProblemDetails();
        }

        private static async Task<IResult> GetOrderByCreatedDate(
        Guid applicationOrderId,
        ISender sender,
        CancellationToken cancellationToken)
        {
            var result = await sender.Send(
                new GetOrderByCreatedDateQuery(applicationOrderId),
                cancellationToken);

            return result.IsSuccess ?
                Results.Ok(result.Value) :
                result.ToProblemDetails();
        }
    }
}
