using Shared.Components.ExceptionHandling.Middleware;
using Shared.Components.Jwt;
using Shared.Components.Migrations;
using User.API.Endpoints;
using User.API.Extensions;
using User.Application.Extensions;
using User.Infrastructure.Context;
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

        builder.Services.ConfigureJWT(builder.Configuration);
        builder.Services.AddAuthorization();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.ApplyMigrations<UserDbContext>();
        }

        app.MapUserEndpoints();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStatusCodePages();

        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

        app.Run();
    }
}

