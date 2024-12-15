using Auth.DAL.Extensions;
using Auth.BLL.Extensions;
using Shared.CleanArchitecture.Presentation.Middleware;
using Auth.PL.Extensions;

namespace Auth.PL;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDataAccessLayer(builder.Configuration);
        builder.Services.AddBusinessLogicLayer();
        builder.Services.AddPresentationLayer();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.ApplyMigrations();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStatusCodePages();
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

        app.MapControllers();

        app.Run();
    }
}
