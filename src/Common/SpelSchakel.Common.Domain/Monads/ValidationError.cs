namespace SpelSchakel.Common.Domain.Monads;

/// <summary>
///     Defines a validation error than can be returned from a method.
/// </summary>
/// <param name="Errors"><see cref="IEnumerable{Error}" /> with errors to wrap.</param>
public record ValidationError(Error[] Errors)
    : Error($"General.{nameof(ValidationError)}", "One or more validation errors occurred.", ErrorType.Validation)
{
    /// <summary>
    ///     Errors wrapped within this validation error.
    /// </summary>
    public Error[] Errors { get; } = Errors;

    /// <summary>
    ///     Creates a new instance of <see cref="ValidationError" /> based on a collection of <see cref="Result" />.
    /// </summary>
    /// <param name="results">
    ///     <see cref="IEnumerable{Result}" /> with <see cref="Result" /> from which to create a new
    ///     instance.
    /// </param>
    /// <returns>A new instance of <see cref="ValidationError" /> with the errors from the given results.</returns>
    public static ValidationError FromResults(IEnumerable<Result> results)
    {
        return new ValidationError(results
            .Where(result => result.IsFailure)
            .Select(result => result.Error)
            .ToArray());
    }
}