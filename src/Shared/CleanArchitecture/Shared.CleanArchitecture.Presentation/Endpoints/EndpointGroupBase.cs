using Carter;

namespace Shared.CleanArchitecture.Presentation.Endpoints;

public abstract class EndpointGroupBase : CarterModule
{
    protected EndpointGroupBase() { }

    protected EndpointGroupBase(string basePath) : base(basePath) { }
}
