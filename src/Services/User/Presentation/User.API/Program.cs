using Shared.CleanArchitecture.Presentation.Middleware;
using User.API.Extensions;
using User.Application.Extensions;
using User.Infrastructure.Extensions;

namespace User.API;

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
            app.ApplyMigrations();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseAuthentication();

        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

        app.UseEndpoints();

        app.Run();
    }
}

