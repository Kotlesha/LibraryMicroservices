using Shared.Components.ExceptionHandling.Middleware;
using Shared.Components.Migrations;
using Order.API.Endpoints;
using Order.API.Extensions;
using Order.Application.Extensions;
using Order.Infrastructure.Context;
using Order.Infrastructure.Extensions;

namespace Order.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddPresentation();

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.ApplyMigrations<OrderDbContext>();
        }

        app.MapOrderEndpoints();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStatusCodePages();

        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

        app.Run();
    }
}

