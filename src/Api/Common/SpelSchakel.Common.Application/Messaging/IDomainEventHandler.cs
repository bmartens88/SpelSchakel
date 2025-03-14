using SpelSchakel.Common.Domain.Abstractions;

namespace SpelSchakel.Common.Application.Messaging;

/// <summary>
///     Represents a handler for a domain event.
/// </summary>
/// <typeparam name="TDomainEvent">The type of the domain event to handle.</typeparam>
public interface IDomainEventHandler<in TDomainEvent> : IDomainEventHandler
{
    /// <summary>
    ///     Handles the domain event.
    /// </summary>
    /// <param name="domainEvent">The domain event to handle.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
}

/// <summary>
///     Represents a handler for a domain event.
/// </summary>
public interface IDomainEventHandler
{
    /// <summary>
    ///     Handles the domain event.
    /// </summary>
    /// <param name="domainEvent">The domain event to handle.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
}