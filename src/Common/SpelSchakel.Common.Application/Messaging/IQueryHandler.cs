using MediatR;
using SpelSchakel.Common.Domain.Monads;

namespace SpelSchakel.Common.Application.Messaging;

/// <summary>
///     Represents a handler for a query that returns a <see cref="Result{TResponse}" />.
/// </summary>
/// <typeparam name="TQuery">The type of the query to handle.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;