namespace SpelSchakel.Common.Domain.Common;

/// <summary>
///     Defines a base class for an entity.
///     An entity is an object that is defined by its identity.
/// </summary>
/// <typeparam name="TId">The type of the identifier. Must be of type <see cref="TypedId" />.</typeparam>
public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : TypedId
{
    /// <summary>
    ///     Constructs a new instance of <see cref="Entity{TId}" />.
    /// </summary>
    /// <param name="id">The value for the identifier.</param>
    protected Entity(TId id)
    {
        Id = id;
    }

    /// <summary>
    ///     Constructs a new instance of <see cref="Entity{TId}" />.
    /// </summary>
#pragma warning disable CS8618
    protected Entity()
    {
        // Used by EF Core for object marshalling.
    }
#pragma warning restore CS8618

    /// <summary>
    ///     The unique identifier of the entity.
    /// </summary>
    public TId Id { get; }

    /// <inheritdoc />
    public bool Equals(Entity<TId>? other)
    {
        return Equals(other as object);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> other
               && Id == other.Id;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    /// <summary>
    ///     Determines whether the specified <see cref="Entity{TId}" /> instances are considered equal.
    /// </summary>
    /// <param name="left">The first <see cref="Entity{TId}" /> to compare.</param>
    /// <param name="right">The second <see cref="Entity{TId}" /> to compare.</param>
    /// <returns>
    ///     <c>true</c> if the specified <see cref="Entity{TId}" /> instances are equal; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        return Equals(left, right);
    }

    /// <summary>
    ///     Determines whether the specified <see cref="Entity{TId}" /> instances are considered not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Entity{TId}" /> to compare.</param>
    /// <param name="right">The second <see cref="Entity{TId}" /> to compare.</param>
    /// <returns>
    ///     <c>true</c> if the specified <see cref="Entity{TId}" /> instances are not equal; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return !Equals(left, right);
    }
}