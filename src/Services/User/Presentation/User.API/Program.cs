using Carter;
using User.API.Extensions;
using User.Application.Extensions;
using User.Infrastructure.Extensions;
using Shared.CleanArchitecture.Presentation.Middleware.Exceptions;

namespace User.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(app.Configuration);
        builder.Services.AddPresentation();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseAuthentication();

        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

        app.MapCarter();

        app.Run();
    }
}
