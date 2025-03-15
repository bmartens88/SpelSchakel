using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SpelSchakel.Common.Presentation.Endpoints;

/// <summary>
///     Class which defines extension method(s) for use with application endpoint(s).
/// </summary>
public static class EndpointExtensions
{
    /// <summary>
    ///     Adds all implementations of <see cref="IEndpoint" /> to the service container.
    /// </summary>
    /// <param name="services">Service container used for Dependency Injection.</param>
    /// <param name="assemblies">Collection of assemblies to scan.</param>
    /// <returns>Service container with registered application endpoint(s).</returns>
    public static IServiceCollection AddEndpoints(this IServiceCollection services, params Assembly[] assemblies)
    {
        var serviceDescriptors = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    /// <summary>
    ///     Maps an endpoint.
    /// </summary>
    /// <param name="app">The web application.</param>
    /// <param name="routeGroupBuilder"><see cref="RouteGroupBuilder" /> for registering application endpoint(s).</param>
    /// <returns><see cref="IApplicationBuilder" /> with registered endpoint(s).</returns>
    public static IApplicationBuilder MapEndpoints(
        this WebApplication app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (var endpoint in endpoints)
            endpoint.MapEndpoint(app);

        return app;
    }
}