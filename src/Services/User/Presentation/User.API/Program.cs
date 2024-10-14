using Carter;
using User.API.Extensions;
using User.Application.Extensions;
using User.Infrastructure.Extensions;
using Shared.CleanArchitecture.Presentation.Middleware.Exceptions;
using Shared.CleanArchitecture.Presentation.Endpoints;
using User.API.Endpoints;
using User.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace User.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddPresentation();

        builder.Services.AddAuthorization(); 
        builder.Services.AddAuthentication();

        var app = builder.Build();

        app.MapGet("/", () => "Hello world");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.ApplyMigrations();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseAuthentication();

        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

        app.MapControllers();
        //app.MapCarter();

        app.Run();
    }
}

