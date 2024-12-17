using Auth.DAL.Extensions;
using Auth.BLL.Extensions;
using Auth.PL.Extensions;
using Shared.Components.ExceptionHandling.Middleware;
using Shared.Components.Migrations;
using Auth.DAL.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Auth.PL;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDataAccessLayer(builder.Configuration);
        builder.Services.AddBusinessLogicLayer();
        builder.Services.AddPresentationLayer();

        builder.Services.ConfigureJWT(builder.Configuration);
        builder.Services.AddAuthorization();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.ApplyMigrations<AccountDbContext>();
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
