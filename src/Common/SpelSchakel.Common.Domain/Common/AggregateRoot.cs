using SpelSchakel.Common.Domain.Abstractions;

namespace SpelSchakel.Common.Domain.Common;

/// <summary>
///     Defines a base class for an aggregate root.
/// </summary>
/// <typeparam name="TId">The type of the identifier. Must be of type <see cref="TypedId" />.</typeparam>
public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    where TId : TypedId
{
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>
    ///     Constructs a new instance of <see cref="AggregateRoot{TId}" />.
    /// </summary>
    /// <param name="id">The value for the identifier.</param>
    protected AggregateRoot(TId id)
        : base(id)
    {
    }

    /// <summary>
    ///     Constructs a new instance of <see cref="AggregateRoot{TId}" />.
    /// </summary>
    protected AggregateRoot()
    {
        // Used by EF Core for object marshalling.
    }

    /// <inheritdoc />
    public IReadOnlyList<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();
        _domainEvents.Clear();

        return copy;
    }

    /// <summary>
    ///     Raises a domain event.
    /// </summary>
    /// <param name="domainEvent">The domain event which was raised.</param>
    protected void RaiseEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}