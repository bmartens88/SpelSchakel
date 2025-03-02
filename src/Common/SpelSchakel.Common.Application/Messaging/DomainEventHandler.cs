using SpelSchakel.Common.Domain.Abstractions;

namespace SpelSchakel.Common.Application.Messaging;

/// <summary>
///     Defines a base class for a domain event handler.
/// </summary>
/// <typeparam name="TDomainEvent">The type of the domain event to handle.</typeparam>
public abstract class DomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    /// <inheritdoc />
    public Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        return Handle((TDomainEvent)domainEvent, cancellationToken);
    }

    /// <inheritdoc />
    public abstract Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
}