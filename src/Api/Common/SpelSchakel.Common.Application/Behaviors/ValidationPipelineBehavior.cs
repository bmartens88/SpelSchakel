using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using SpelSchakel.Common.Application.Messaging;
using SpelSchakel.Common.Domain.Monads;

namespace SpelSchakel.Common.Application.Behaviors;

/// <summary>
///     Defines an <see cref="IPipelineBehavior{TRequest,TResponse}" /> implementation which performs validation actions
///     against a request of type <typeparamref name="TRequest" /> and returns a <see cref="ValidationError" /> if any
///     validation errors occur.
/// </summary>
/// <param name="logger"><see cref="ILogger{TCategoryName}" /> for logging purposes.</param>
/// <param name="validators">Collection of <see cref="IValidator{TRequest}" /> validators for the current request.</param>
/// <typeparam name="TRequest">The type of the request handled.</typeparam>
/// <typeparam name="TResponse">The type of the response handled.</typeparam>
/// <note>Only requests which are an instance of <see cref="ICommand{TResponse}" /> are validated.</note>
internal sealed class ValidationPipelineBehavior<TRequest, TResponse>(
    ILogger<ValidationPipelineBehavior<TRequest, TResponse>> logger,
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    // IDE might show this as unnecessary, but this ensures the reference will not ever change
    private readonly ILogger<ValidationPipelineBehavior<TRequest, TResponse>> _logger = logger;
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var validationFailures = await ValidateAsync(request, _validators.ToList(), cancellationToken);

        if (validationFailures.Length is 0)
            return await next();

        _logger.LogError("Returning {ValidationCount} of validation error(s) for request {RequestName}",
            validationFailures.Length,
            typeof(TRequest).Name);

        if (typeof(TResponse).IsGenericType
            && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            // Get the generic type
            var resultType = typeof(TResponse).GetGenericArguments()[0];

            var failureMethod = typeof(Result<>)
                .MakeGenericType(resultType)
                .GetMethod(nameof(Result<object>.ValidationFailure));

            if (failureMethod is not null)
                return (TResponse)failureMethod.Invoke(null, [CreateValidationError(validationFailures)])!;
        }
        else if (typeof(TResponse) == typeof(Result))
        {
            // Simply return Failure with validation error
            return (TResponse)(object)Result.Failure(CreateValidationError(validationFailures));
        }

        throw new ValidationException(validationFailures);
    }

    /// <summary>
    ///     Performs validation on the current request of type <typeparamref name="TRequest" />.
    /// </summary>
    /// <param name="request">The current request to validate.</param>
    /// <param name="validators">Collection of <see cref="IValidator{TRequest}" /> validators for request validation.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A collection of <see cref="ValidationFailure" /> returned by validators.</returns>
    private static async Task<ValidationFailure[]> ValidateAsync(TRequest request,
        List<IValidator<TRequest>> validators,
        CancellationToken cancellationToken = default)
    {
        if (validators.Count is 0)
            return [];

        var context = new ValidationContext<TRequest>(request);

        var validateResults = await Task.WhenAll(validators
            .Select(validator => validator.ValidateAsync(context, cancellationToken)));

        var validationFailures = validateResults
            .Where(result => !result.IsValid)
            .SelectMany(result => result.Errors)
            .ToArray();

        return validationFailures;
    }

    /// <summary>
    ///     Returns a new instance of <see cref="ValidationError" /> based on validation error(s).
    /// </summary>
    /// <param name="validationFailures">Collection of <see cref="ValidationFailure" /> collected during validation.</param>
    /// <returns><see cref="ValidationError" /> with all validation error(s) collected during validation.</returns>
    private static ValidationError CreateValidationError(ValidationFailure[] validationFailures)
    {
        return new ValidationError(validationFailures
            .Select(failure => Error.Problem(failure.ErrorCode, failure.ErrorMessage))
            .ToArray());
    }
}