using System.Diagnostics.CodeAnalysis;

namespace SpelSchakel.Common.Domain.Monads;

/// <summary>
///     Defines a result which can be returned from a method.
/// </summary>
public class Result
{
    /// <summary>
    ///     Constructs a new instance of <see cref="Result" />.
    /// </summary>
    /// <param name="isSuccess">Whether the result was successful.</param>
    /// <param name="error">Any error which might have occurred.</param>
    /// <exception cref="ArgumentException">Thrown when an invalid value for <paramref name="error" /> is provided.</exception>
    public Result(bool isSuccess, Error error)
    {
        if ((isSuccess && error != Error.None)
            || (!isSuccess && error == Error.None))
            throw new ArgumentException("Invalid error", nameof(error));

        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    ///     Whether the result was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    ///     Whether the result was a failure.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    ///     The error returned by the result.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    ///     Factory method to create a new instance of <see cref="Result" /> which was successful.
    /// </summary>
    /// <returns>A new instance of a successful <see cref="Result" />.</returns>
    public static Result Success()
    {
        return new Result(true, Error.None);
    }

    /// <summary>
    ///     Factory method to create a new instance of <see cref="Result{TValue}" /> which was successful.
    /// </summary>
    /// <param name="value">Data which is returned by the <see cref="Result{TValue}" />.</param>
    /// <typeparam name="TValue">Type of the data which is returned.</typeparam>
    /// <returns>A new instance of a successful <see cref="Result{TValue}" />.</returns>
    public static Result<TValue> Success<TValue>(TValue value)
    {
        return new Result<TValue>(value, true, Error.None);
    }

    /// <summary>
    ///     Factory method to create a new instance of <see cref="Result" /> which was a failure.
    /// </summary>
    /// <param name="error">The error which is being returned.</param>
    /// <returns>A new instance of a failed <see cref="Result" />.</returns>
    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }

    /// <summary>
    ///     Factory method to create a new instance of <see cref="Result{TValue}" /> which was a failure.
    /// </summary>
    /// <param name="error">The error which is being returned.</param>
    /// <typeparam name="TValue">The type of the data which would have been returned.</typeparam>
    /// <returns>A new instance of a failed <see cref="Result{TValue}" />.</returns>
    public static Result<TValue> Failure<TValue>(Error error)
    {
        return new Result<TValue>(default, false, error);
    }
}

/// <summary>
///     Defines a result which can be returned from a method, which also holds some data.
/// </summary>
/// <param name="value">The data to be returned.</param>
/// <param name="isSuccess">Whether the <see cref="Result{TValue}" /> is a success.</param>
/// <param name="error">In case of a failure, the error to be returned.</param>
/// <typeparam name="TValue">Type of the data which is returned.</typeparam>
public class Result<TValue>(TValue? value, bool isSuccess, Error error)
    : Result(isSuccess, error)
{
    private readonly TValue? _value = value;

    /// <summary>
    ///     Returns the value of this <see cref="Result{TValue}" />.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown in case the <see cref="Result{TValue}" /> was a failure, since no data is available.
    /// </exception>
    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failed result can't be accessed.");

    /// <summary>
    ///     Implicitly converts a <typeparamref name="TValue" /> to a <see cref="Result{TValue}" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="Result{TValue}" /> containing the specified value.</returns>
    public static implicit operator Result<TValue>(TValue? value)
    {
        return value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
    }

    /// <summary>
    ///     Factory method to create a new instance of <see cref="Result{TValue}" /> which was a failure.
    /// </summary>
    /// <param name="error">The error to return.</param>
    /// <returns>A <see cref="Result{TValue}" /> containing the specified error.</returns>
    public static Result<TValue> ValidationFailure(Error error)
    {
        return new Result<TValue>(default, false, error);
    }
}