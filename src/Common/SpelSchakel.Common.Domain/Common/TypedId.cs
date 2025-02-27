using Ardalis.GuardClauses;

namespace SpelSchakel.Common.Domain.Common;

/// <summary>
///     Defines a base class for a strongly-typed identifier.
/// </summary>
/// <param name="value">The value of the identifier.</param>
/// <typeparam name="TValue">The type of the value.</typeparam>
public abstract class TypedId<TValue>(TValue value) : TypedId
    where TValue : struct
{
    /// <summary>
    ///     The actual value of the identifier.
    /// </summary>
    public TValue Value { get; } = Guard.Against.Default(value);

    /// <inheritdoc />
    protected sealed override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

/// <summary>
///     Defines an abstract class which is primarily used for abstraction purposes.
/// </summary>
public abstract class TypedId : ValueObject;