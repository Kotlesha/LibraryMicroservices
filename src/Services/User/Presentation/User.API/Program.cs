using Serilog;
using Shared.Components.ExceptionHandling.Middleware;
using Shared.Components.Jwt;
using User.API.Extensions;
using User.Application.Extensions;
using User.Infrastructure.Extensions;

namespace User.API;

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

