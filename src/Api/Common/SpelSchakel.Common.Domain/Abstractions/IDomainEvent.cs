using MediatR;

namespace SpelSchakel.Common.Domain.Abstractions;

/// <summary>
///     Defines the contract for a domain event.
/// </summary>
public interface IDomainEvent : INotification
{
    /// <summary>
    ///     The moment in time when the event occurred.
    /// </summary>
    DateTime OccurredOnUtc { get; }
}