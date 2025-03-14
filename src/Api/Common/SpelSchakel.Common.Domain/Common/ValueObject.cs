namespace SpelSchakel.Common.Domain.Common;

/// <summary>
///     Defines a base class for a value object.
///     Value objects are objects that are defined by their attributes, not by their identity.
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>
{
    /// <inheritdoc cref="IEquatable{T}.Equals(T?)" />
    public bool Equals(ValueObject? other)
    {
        return Equals(other as object);
    }

    /// <summary>
    ///     Provides the components that are used to determine equality for the derived class.
    /// </summary>
    /// <returns>
    ///     An <see cref="IEnumerable{Object}" /> containing the components that are used to determine equality.
    /// </returns>
    protected abstract IEnumerable<object> GetEqualityComponents();

    /// <inheritdoc cref="object.Equals(object?)" />
    public override bool Equals(object? obj)
    {
        return obj is ValueObject other
               && GetEqualityComponents()
                   .SequenceEqual(other.GetEqualityComponents());
    }

    /// <inheritdoc cref="object.GetHashCode" />
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x.GetHashCode())
            .Aggregate((x, y) => x ^ y);
    }

    /// <summary>
    ///     Determines whether the specified <see cref="ValueObject" /> instances are considered equal.
    /// </summary>
    /// <param name="left">The first <see cref="ValueObject" /> to compare.</param>
    /// <param name="right">The second <see cref="ValueObject" /> to compare.</param>
    /// <returns>
    ///     <c>true</c> if the specified <see cref="ValueObject" /> instances are equal; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        return Equals(left, right);
    }

    /// <summary>
    ///     Determines whether the specified <see cref="ValueObject" /> instances are considered not equal.
    /// </summary>
    /// <param name="left">The first <see cref="ValueObject" /> to compare.</param>
    /// <param name="right">The second <see cref="ValueObject" /> to compare.</param>
    /// <returns>
    ///     <c>true</c> if the specified <see cref="ValueObject" /> instances are not equal; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !Equals(left, right);
    }
}