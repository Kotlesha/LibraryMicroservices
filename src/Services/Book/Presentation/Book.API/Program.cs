using Shared.Components.ExceptionHandling.Middleware;
using Shared.Components.Migrations;
using Book.API.Endpoints;
using Book.API.Extensions;
using Book.Application.Extensions;
using Book.Infrastructure.Contexts;
using Book.Infrastructure.Extensions;
using Serilog;
using Shared.Components.Jwt;

namespace Book.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, loggerConfig) =>
            loggerConfig.ReadFrom.Configuration(context.Configuration));

        builder.Services.AddApplication(builder.Configuration);
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddPresentation();

        builder.Services.AddJwtAuthentication(builder.Configuration);
        builder.Services.AddAuthorization();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            //app.ApplyMigrations<BookDbContext>();
        }

        app.MapEndpoints();

        app.UseHttpsRedirection();

        app.UseSerilogRequestLogging();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStatusCodePages();

        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

        app.Run();
    }
}

