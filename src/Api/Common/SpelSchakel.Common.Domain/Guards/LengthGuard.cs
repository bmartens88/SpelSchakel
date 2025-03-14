using System.Runtime.CompilerServices;
using Ardalis.GuardClauses;

namespace SpelSchakel.Common.Domain.Guards;

/// <summary>
///     Provides a guard clause for checking the length of a string.
/// </summary>
public static class LengthGuard
{
    /// <summary>
    ///     Guards the input string to ensure it is not beyond a given maximum length.
    /// </summary>
    /// <param name="_"><see cref="IGuardClause" /> instance. Not used here.</param>
    /// <param name="input">The input to validate/guard.</param>
    /// <param name="maxLength">The maximum length that <paramref name="input" /> can't exceed.</param>
    /// <param name="paramName">The name of the parameter with which <paramref name="input" /> corresponds.</param>
    /// <returns>
    ///     <paramref name="input" /> if its Length property is not greater than or equal to <paramref name="maxLength" />.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///     Thrown when the Length property of <paramref name="input" /> is greater than or equal to
    ///     <paramref name="maxLength" />.
    /// </exception>
    public static string Length(this IGuardClause _,
        string input,
        int maxLength,
        [CallerArgumentExpression(nameof(input))]
        string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(input);
        if (input.Length >= maxLength)
            throw new ArgumentException($"Should not exceed maximum length of {maxLength} characters.", paramName);
        return input;
    }
}