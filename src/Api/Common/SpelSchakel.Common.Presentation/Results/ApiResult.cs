using Microsoft.AspNetCore.Http;
using SpelSchakel.Common.Domain.Monads;

namespace SpelSchakel.Common.Presentation.Results;

/// <summary>
///     Class containing definition of method to create <see cref="IResult" /> instances.
/// </summary>
public static class ApiResult
{
    /// <summary>
    ///     Generates a problem details object from the provided <see cref="Result" /> object.
    /// </summary>
    /// <param name="result">The result of the operation.</param>
    /// <returns><see cref="IResult" /> with problem details object.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the result of the operation was successful.</exception>
    public static IResult Problem(Result result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException();

        return Microsoft.AspNetCore.Http.Results.Problem(
            title: GetTitle(result.Error),
            detail: GetDetail(result.Error),
            type: GetType(result.Error.ErrorType),
            statusCode: GetStatusCode(result.Error.ErrorType),
            extensions: GetErrors(result));

        // Get a title based on the error.
        static string GetTitle(Error error)
        {
            return error.ErrorType switch
            {
                ErrorType.Validation => error.Code,
                ErrorType.Problem => error.Code,
                ErrorType.NotFound => error.Code,
                ErrorType.Conflict => error.Code,
                _ => "Server failure"
            };
        }

        // Get a details string based on the error.
        static string GetDetail(Error error)
        {
            return error.ErrorType switch
            {
                ErrorType.Validation => error.Description,
                ErrorType.Problem => error.Description,
                ErrorType.NotFound => error.Description,
                ErrorType.Conflict => error.Description,
                _ => "An unexpected error occurred"
            };
        }

        // Get a type based on the error type.
        static string GetType(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.Problem => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };
        }

        // Get the status code based on the error type.
        static int GetStatusCode(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Problem => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };
        }

        // Any additional information which is added as an extension to the problem details object.
        static Dictionary<string, object?>? GetErrors(Result result)
        {
            if (result.Error is not ValidationError validationError) return null;

            return new Dictionary<string, object?>
            {
                { "errors", validationError.Errors }
            };
        }
    }
}