using MediatR;
using SpelSchakel.Common.Domain.Monads;

namespace SpelSchakel.Common.Application.Messaging;

/// <summary>
///     Represents a query that returns a <see cref="Result{TResponse}" />.
/// </summary>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>;