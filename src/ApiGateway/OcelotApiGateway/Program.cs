using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using OcelotApiGateway.Extensions;
using Shared.Components.Jwt;

namespace OcelotApiGateway;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.ConfigureJWT(builder.Configuration);
        builder.Services.AddAuthorization();

        builder.Services.AddExtendedProblemDetailsWithOcelot();

        builder.Configuration.AddJsonFile("configuration.json", optional: false, reloadOnChange: true);
        builder.Services.AddOcelot();

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStatusCodePages();

        await app.UseOcelot();

        //app.UseStatusCodePages();

        app.Run();
    }
}
