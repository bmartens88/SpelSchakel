using MediatR;
using SpelSchakel.Common.Domain.Monads;

namespace SpelSchakel.Common.Application.Messaging;

/// <summary>
///     Represents a handler for a command that returns a <see cref="Result" />.
/// </summary>
/// <typeparam name="TCommand">The type of the command to handle.</typeparam>
public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;

/// <summary>
///     Represents a handler for a command that returns a <see cref="Result{TResponse}" />.
/// </summary>
/// <typeparam name="TCommand">The type of the command to handle.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;