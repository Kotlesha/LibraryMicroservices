using Shared.Components.ExceptionHandling.Middleware;
using Order.API.Extensions;
using Order.Application.Extensions;
using Order.Infrastructure.Extensions;
using Serilog;
using Shared.Components.Jwt;

namespace Order.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, loggerConfig) =>
            loggerConfig.ReadFrom.Configuration(context.Configuration));

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddPresentation();

        builder.Services.AddJwtAuthentication(builder.Configuration);
        builder.Services.AddAuthorization();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
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

