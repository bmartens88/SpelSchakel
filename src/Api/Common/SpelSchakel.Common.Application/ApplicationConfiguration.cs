using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SpelSchakel.Common.Application.Behaviors;

namespace SpelSchakel.Common.Application;

/// <summary>
///     Defines extension method(s) for configuring the application layer.
/// </summary>
public static class ApplicationConfiguration
{
    /// <summary>
    ///     Registers services for the application layer.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection" /> for service registration.</param>
    /// <param name="moduleAssemblies">
    ///     Collection of <see cref="Assembly" /> representing the modules to register application
    ///     services for.
    /// </param>
    /// <returns><see cref="IServiceCollection" /> with services configured.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services,
        Assembly[] moduleAssemblies)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(moduleAssemblies);

            configuration.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssemblies(moduleAssemblies, includeInternalTypes: true);

        return services;
    }
}