namespace SpelSchakel.Common.Domain.Monads;

/// <summary>
///     Represents different types of errors that can occur.
/// </summary>
public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    NotFound = 2,
    Problem = 3,
    Conflict = 4
}