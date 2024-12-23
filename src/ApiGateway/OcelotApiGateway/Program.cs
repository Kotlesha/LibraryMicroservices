using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using OcelotApiGateway.Extensions;
using Serilog;
using Shared.Components.Jwt;

namespace OcelotApiGateway;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, loggerConfig) =>
            loggerConfig.ReadFrom.Configuration(context.Configuration));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddJwtAuthentication(builder.Configuration);
        builder.Services.AddAuthorization();

        builder.Services.AddExtendedProblemDetailsWithOcelot();

        builder.Configuration.AddJsonFile("configuration.json", optional: false, reloadOnChange: true);
        builder.Services.AddOcelot();
        builder.Services.AddSwaggerForOcelot(builder.Configuration);

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseSerilogRequestLogging();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStatusCodePages();

        app.UseSwaggerForOcelotUI();

        await app.UseOcelot();

        app.Run();
    }
}
