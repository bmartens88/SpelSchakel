using MediatR;
using Microsoft.Extensions.Logging;
using SpelSchakel.Common.Application.Exceptions;

namespace SpelSchakel.Common.Application.Behaviors;

/// <summary>
///     Defines an <see cref="IPipelineBehavior{TRequest,TResponse}" /> implementation for global exception handling.
/// </summary>
/// <param name="logger"><see cref="ILogger{TCategoryName}" /> for logging purposes.</param>
/// <typeparam name="TRequest">Type of the request handled.</typeparam>
/// <typeparam name="TResponse">Type of the response handled.</typeparam>
internal sealed class ExceptionHandlingPipelineBehavior<TRequest, TResponse>(
    ILogger<ExceptionHandlingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    // IDE might show this as unnecessary, but this ensures the reference will not ever change
    private readonly ILogger<ExceptionHandlingPipelineBehavior<TRequest, TResponse>> _logger = logger;

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogError(ex, "Exception thrown during processing of request {RequestName}", requestName);
            throw new SpelSchakelException(requestName, innerException: ex);
        }
    }
}