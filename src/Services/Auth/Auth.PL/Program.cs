using Auth.DAL.Extensions;
using Auth.BLL.Extensions;
using Auth.PL.Extensions;
using Shared.Components.ExceptionHandling.Middleware;
using Shared.Components.Jwt;
using Serilog;

namespace Auth.PL;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, loggerConfig) =>
            loggerConfig.ReadFrom.Configuration(context.Configuration));

        builder.Services.AddDataAccessLayer(builder.Configuration);
        builder.Services.AddBusinessLogicLayer(builder.Configuration);
        builder.Services.AddPresentationLayer();

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
