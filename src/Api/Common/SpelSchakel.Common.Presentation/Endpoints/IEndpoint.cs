using Microsoft.AspNetCore.Routing;

namespace SpelSchakel.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}