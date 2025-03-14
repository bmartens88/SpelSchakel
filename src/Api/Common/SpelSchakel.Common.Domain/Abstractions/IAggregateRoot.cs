namespace SpelSchakel.Common.Domain.Abstractions;

/// <summary>
///     Defines the contract for an aggregate root.
/// </summary>
public interface IAggregateRoot
{
    /// <summary>
    ///     Returns a copy of all domain events for the aggregate root and clears the list.
    /// </summary>
    /// <returns><see cref="IReadOnlyList{IDomainEvent}" /> containing the domain events for the aggregate root.</returns>
    IReadOnlyList<IDomainEvent> PopDomainEvents();
}