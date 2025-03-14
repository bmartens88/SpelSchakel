namespace SpelSchakel.Common.Domain.Enums;

/// <summary>
///     Represents a base class for creating smart enumerations with a name and integer value.
/// </summary>
/// <param name="Name">The name of the enumeration.</param>
/// <param name="Value">The integer value associated with the enumeration.</param>
/// <typeparam name="TEnum">The type of the smart enumeration.</typeparam>
public abstract record SmartEnum<TEnum>(string Name, int Value) : SmartEnum<TEnum, int>(Name, Value)
    where TEnum : SmartEnum<TEnum, int>;

/// <summary>
///     Represents a base class for creating smart enumerations.
/// </summary>
/// <typeparam name="TEnum">The type of the smart enumeration.</typeparam>
/// <typeparam name="TValue">The type of the value associated with the enumeration.</typeparam>
public abstract record SmartEnum<TEnum, TValue>
    where TEnum : SmartEnum<TEnum, TValue>
    where TValue : IEquatable<TValue>
{
    private const string InvalidInputMsg = "Invalid value for input.";
    private readonly List<TEnum> _enumValues = [];

    /// <summary>
    ///     Constructor for a smart enumeration.
    /// </summary>
    /// <param name="name">The name of the enumeration.</param>
    /// <param name="value">The value associated with the enumeration.</param>
    protected SmartEnum(string name, TValue value)
    {
        Name = name;
        Value = value;
        _enumValues.Add((TEnum)this);
    }

    public string Name { get; }
    public TValue Value { get; }

    /// <summary>
    ///     Attempts to retrieve a smart enumeration instance by its name.
    /// </summary>
    /// <param name="name">The name of the enumeration to find.</param>
    /// <param name="smartEnum">
    ///     When this method returns, contains the smart enumeration instance associated with the specified
    ///     name, if found; otherwise, <c>null</c>.
    /// </param>
    /// <returns><c>true</c> if a smart enumeration instance with the specified name is found; otherwise, <c>false</c>.</returns>
    public bool TryFromName(string name, out TEnum? smartEnum)
    {
        smartEnum = _enumValues
            .FirstOrDefault(val => val.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        return smartEnum is not null;
    }

    /// <summary>
    ///     Attempts to retrieve a smart enumeration instance by its value.
    /// </summary>
    /// <param name="value">The value of the enumeration to find.</param>
    /// <param name="smartEnum">
    ///     When this method returns, contains the smart enumeration instance associated with the specified
    ///     value, if found; otherwise, <c>null</c>.
    /// </param>
    /// <returns><c>true</c> if a smart enumeration instance witht eh specified value is found; otherwise, <c>false</c>.</returns>
    public bool TryFromValue(TValue value, out TEnum? smartEnum)
    {
        smartEnum = _enumValues
            .FirstOrDefault(val => val.Value.Equals(value));
        return smartEnum is not null;
    }

    /// <summary>
    ///     Retrieves a smart enumeration instance by its name.
    /// </summary>
    /// <param name="name">The name of the enumeration to find.</param>
    /// <returns>The smart enumeration instance associated with the specified name.</returns>
    /// <exception cref="ArgumentException">Thrown when no enumeration instance with the specified name is found.</exception>
    public TEnum FromName(string name)
    {
        return _enumValues
                   .FirstOrDefault(val => val.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ??
               throw new ArgumentException(InvalidInputMsg, nameof(name));
    }

    /// <summary>
    ///     Retrieves a smart enumeration instance by its value.
    /// </summary>
    /// <param name="value">The value of the enumeration to find.</param>
    /// <returns>The smart enumeration instance associated with the specified value.</returns>
    /// <exception cref="ArgumentException">Thrown when no enumeration instance with the specified value is found.</exception>
    public TEnum FromValue(TValue value)
    {
        return _enumValues
                   .FirstOrDefault(val => val.Value.Equals(value)) ??
               throw new ArgumentException(InvalidInputMsg, nameof(value));
    }

    /// <inheritdoc />
    public sealed override string ToString()
    {
        return Name;
    }
}