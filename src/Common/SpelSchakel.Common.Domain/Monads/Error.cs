namespace SpelSchakel.Common.Domain.Monads;

/// <summary>
///     Defines an error that can be returned by a function.
/// </summary>
/// <param name="Code">The error code.</param>
/// <param name="Description">A short description of the error.</param>
/// <param name="ErrorType">The type of error.</param>
public record Error(string Code, string Description, ErrorType ErrorType)
{
    /// <summary>
    ///     An error which represents no error.
    /// </summary>
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

    /// <summary>
    ///     Error returned when a null value is provided.
    /// </summary>
    public static readonly Error NullValue = new(
        $"General.{nameof(NullValue)}",
        "A null value was provided",
        ErrorType.Failure);

    /// <summary>
    ///     The code of the error.
    /// </summary>
    public string Code { get; } = Code;

    /// <summary>
    ///     A short description of the error.
    /// </summary>
    public string Description { get; } = Description;

    /// <summary>
    ///     The type of error.
    /// </summary>
    public ErrorType ErrorType { get; } = ErrorType;

    /// <summary>
    ///     Factory method to create an error with type <see cref="Monads.ErrorType.Failure" />.
    /// </summary>
    /// <param name="code">The code of the error.</param>
    /// <param name="description">A short description of the error.</param>
    /// <returns><see cref="Error" /> instance of type <see cref="Monads.ErrorType.Failure" />.</returns>
    public static Error Failure(string code, string description)
    {
        return new Error(code, description, ErrorType.Failure);
    }

    /// <summary>
    ///     Factory method to create an error with type <see cref="Monads.ErrorType.NotFound" />.
    /// </summary>
    /// <param name="code">The code of the error.</param>
    /// <param name="description">A short description of the error.</param>
    /// <returns><see cref="Error" /> instance of type <see cref="Monads.ErrorType.NotFound" />.</returns>
    public static Error NotFound(string code, string description)
    {
        return new Error(code, description, ErrorType.NotFound);
    }

    /// <summary>
    ///     Factory method to create an error with type <see cref="Monads.ErrorType.Problem" />.
    /// </summary>
    /// <param name="code">The code of the error.</param>
    /// <param name="description">A short description of the error.</param>
    /// <returns><see cref="Error" /> instance of type <see cref="Monads.ErrorType.Problem" />.</returns>
    public static Error Problem(string code, string description)
    {
        return new Error(code, description, ErrorType.Problem);
    }

    /// <summary>
    ///     Factory method to create an error with type <see cref="Monads.ErrorType.Conflict" />.
    /// </summary>
    /// <param name="code">The code of the error.</param>
    /// <param name="description">A short description of the error.</param>
    /// <returns><see cref="Error" /> instance of type <see cref="Monads.ErrorType.Conflict" />.</returns>
    public static Error Conflict(string code, string description)
    {
        return new Error(code, description, ErrorType.Conflict);
    }
}