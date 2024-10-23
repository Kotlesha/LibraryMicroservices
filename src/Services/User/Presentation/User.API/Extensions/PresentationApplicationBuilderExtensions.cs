using FastEndpoints;
using FastEndpoints.Swagger;

namespace User.API.Extensions;

public static class PresentationApplicationBuilderExtensions
{
    public static IApplicationBuilder UseEndpoints(this IApplicationBuilder app)
    {
        app.UseFastEndpoints(c =>
            c.Endpoints.RoutePrefix = "users")
            .UseSwaggerGen();

        return app;
    }
}
