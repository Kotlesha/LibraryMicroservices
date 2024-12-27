using Book.API.Endpoints;

namespace Book.API.Extensions;

public static class EndpointRouteBuilder
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/books-api");

        group.MapBookEndpoints();
        group.MapAuthorEndpoints();
        group.MapCategoryEndpoints();
        group.MapGenreEndpoints();

        return group;
    }
}
