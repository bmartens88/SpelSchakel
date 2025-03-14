using MediatR;
using SpelSchakel.Common.Domain.Monads;

namespace SpelSchakel.Common.Application.Messaging;

/// <summary>
///     Represents a command that returns a <see cref="Result" />.
/// </summary>
public interface ICommand : IRequest<Result>, IBaseCommand;

/// <summary>
///     Represents a command that returns a <see cref="Result{TResponse}" />.
/// </summary>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

/// <summary>
///     Interface which serves for abstraction purposes.
/// </summary>
public interface IBaseCommand;