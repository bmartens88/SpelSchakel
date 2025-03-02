using SpelSchakel.Common.Domain.Monads;

namespace SpelSchakel.Common.Application.Exceptions;

/// <summary>
///     Defines a generic exception for the application.
/// </summary>
/// <param name="requestName">The name of the request which threw the exception.</param>
/// <param name="error">Optional <see cref="Err"/></param>
/// <param name="innerException"></param>
public sealed class SpelSchakelException(string requestName, Error? error, Exception? innerException)
    : Exception("Application error", innerException)
{
    public string RequestName { get; } = requestName;

    public Error? Error { get; } = error;
}