﻿using SpelSchakel.Common.Domain.Monads;

namespace SpelSchakel.Common.Presentation.Results;

/// <summary>
///     Class containing extension method(s) for the <see cref="Result" /> and <see cref="Result{TValue}" /> classes.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    ///     Matches the result and executes the corresponding function.
    /// </summary>
    /// <typeparam name="TOut">The type of the output.</typeparam>
    /// <param name="result">The result to match.</param>
    /// <param name="onSuccess">The function to execute if the result is successful.</param>
    /// <param name="onFailure">The function to execute if the result is a failure.</param>
    /// <returns>The output of the executed function.</returns>
    public static TOut Match<TOut>(this Result result,
        Func<TOut> onSuccess,
        Func<Result, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result);
    }

    /// <summary>
    ///     Matches the result and executes the corresponding function.
    /// </summary>
    /// <param name="result">The result to match.</param>
    /// <param name="onSuccess">The function to execute if the result is successful.</param>
    /// <param name="onFailure">The function to execute if the result is a failure.</param>
    /// <typeparam name="TIn">The type of the input.</typeparam>
    /// <typeparam name="TOut">The type of the output.</typeparam>
    /// <returns>The output of the executed function.</returns>
    public static TOut Match<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> onSuccess,
        Func<Result<TIn>, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
    }
}