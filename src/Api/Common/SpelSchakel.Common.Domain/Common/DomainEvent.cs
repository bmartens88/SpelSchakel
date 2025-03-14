using SpelSchakel.Common.Domain.Abstractions;

namespace SpelSchakel.Common.Domain.Common;

/// <summary>
///     Defines a base class for all domain events.
/// </summary>
/// <param name="OccurredOnUtc"><see cref="DateTime" /> representing the point in time the event occurred (UTC).</param>
public abstract record DomainEvent(DateTime OccurredOnUtc) : IDomainEvent
{
    /// <summary>
    ///     Constructor which defaults the <see cref="OccurredOnUtc" /> to the current date and time in UTC.
    /// </summary>
    protected DomainEvent()
        : this(DateTime.UtcNow)
    {
    }
}