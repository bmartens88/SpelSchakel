using Microsoft.AspNetCore.Routing;

namespace SpelSchakel.Common.Presentation.Endpoints;

/// <summary>
///     Defines the contract for an application endpoint.
/// </summary>
public interface IEndpoint
{
    /// <summary>
    ///     Register the endpoint with the <see cref="IEndpointRouteBuilder" /> object.
    /// </summary>
    /// <param name="app">Builder used for endpoint registration.</param>
    void MapEndpoint(IEndpointRouteBuilder app);
}