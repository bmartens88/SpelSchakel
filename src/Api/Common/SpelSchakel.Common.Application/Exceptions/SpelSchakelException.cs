using SpelSchakel.Common.Domain.Monads;

namespace SpelSchakel.Common.Application.Exceptions;

/// <summary>
///     Defines a generic exception for the application.
/// </summary>
/// <param name="requestName">The name of the request which threw the exception.</param>
/// <param name="error">Optional <see cref="SpelSchakel.Common.Domain.Monads.Error" /> to return.</param>
/// <param name="innerException">The <see cref="Exception" /> which was thrown, wrapped inside this exception.</param>
public sealed class SpelSchakelException(string requestName, Error? error = null, Exception? innerException = null)
    : Exception("Application error", innerException)
{
    public string RequestName { get; } = requestName;

    public Error? Error { get; } = error;
}