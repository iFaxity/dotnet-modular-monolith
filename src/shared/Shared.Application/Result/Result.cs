using System.Runtime.CompilerServices;

namespace Shared.Application;

/// <summary>
/// Represents a result of some operation, with status information and possibly an error.
/// </summary>
internal readonly record struct Result : IResult
{
    private readonly IResult _state;

    public Result()
    {
        _state = new Success();
    }

    private Result(IError error)
    {
        _state = new Failure(error);
    }

    public bool IsSuccess => _state.IsSuccess;
    public bool IsFailure => _state.IsFailure;

    /// <summary>
    /// Returns a success <see cref="IResult"/>.
    /// </summary>
    /// <returns>A new instance of <see cref="IResult"/> with the success flag set.</returns>
    public static IResult Success() => new Result();

    /// <summary>
    /// Returns a success <see cref="IResult{T}"/> with the specified value.
    /// </summary>
    /// <typeparam name="T">The result type.</typeparam>
    /// <param name="value">The result value.</param>
    /// <returns>A new instance of <see cref="IResult{T}"/> with the success flag set.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IResult<T> Success<T>(T value)
        where T : notnull => new Result<T>(value);

    /// <summary>
    /// Returns a failure <see cref="IResult"/> with the specified error.
    /// </summary>
    /// <param name="error">The error.</param>
    /// <returns>A new instance of <see cref="IResult"/> with the specified error and failure flag set.</returns>
    public static IResult Failure(IError error)
    {
        if (DomainError.IsNullOrNone(error))
            throw new ArgumentNullException(nameof(error), "Error cannot be None or null");

        return new Result(error);
    }

    /// <summary>
    /// Returns a failure <see cref="IResult"/> with the specified error.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The error message.</param>
    /// <returns>A new instance of <see cref="IResult"/> with the specified error and failure flag set.</returns>
    public static IResult Failure(string code, string message)
    {
        DomainError error = new(code, message);

        return new Result(error);
    }

    /// <summary>
    /// Returns a failure <see cref="IResult{T}"/> with the specified error.
    /// </summary>
    /// <typeparam name="T">The result type.</typeparam>
    /// <param name="error">The error.</param>
    /// <returns>A new instance of <see cref="IResult{T}"/> with the specified error and failure flag set.</returns>
    /// <remarks>
    /// We're purposefully ignoring the nullable assignment here because the API will never allow it to be accessed.
    /// The value is accessed through a method that will throw an exception if the result is a failure result.
    /// </remarks>
    public static IResult<T> Failure<T>(IError error)
        where T : notnull
    {
        if (DomainError.IsNullOrNone(error))
            throw new ArgumentNullException(nameof(error), "Error cannot be None or null");

        return new Result<T>(error);
    }

    /// <summary>
    /// Returns a failure <see cref="IResult{T}"/> with the specified error.
    /// </summary>
    /// <typeparam name="T">The result type.</typeparam>
    /// <param name="code">The error code.</param>
    /// <param name="message">The error message.</param>
    /// <returns>A new instance of <see cref="IResult{T}"/> with the specified error and failure flag set.</returns>
    /// <remarks>
    /// We're purposefully ignoring the nullable assignment here because the API will never allow it to be accessed.
    /// The value is accessed through a method that will throw an exception if the result is a failure result.
    /// </remarks>
    public static IResult<T> Failure<T>(string code, string message)
        where T : notnull
    {
        DomainError error = new(code, message);

        return new Result<T>(error);
    }

    /// <summary>
    ///  If the <paramref name="condition"/> is <c>true</c> then initializes a new successful instance
    /// of <see cref="IResult"/>, otherwise return a failed instance of <see cref="IResult"/>
    /// with the given <paramref name="error"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IResult Ensure(bool condition, Func<IError> error)
    {
        return condition ? Success() : Failure(error());
    }

    /// <summary>
    ///  If the <paramref name="predicate"/> evaluates to <c>true</c> then initializes a new successful instance
    /// of <see cref="IResult"/>, otherwise return a failed instance of <see cref="IResult"/>
    /// with the given <paramref name="error"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IResult Ensure(Func<bool> predicate, Func<IError> error)
    {
        return predicate() ? Success() : Failure(error());
    }

    /// <summary>
    /// If the <paramref name="predicate"/> evaluates to <c>true</c> then initializes a new successful instance
    /// of <see cref="IResult"/>, otherwise return a failed instance of <see cref="IResult"/>
    /// with the given <paramref name="error"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<IResult> EnsureAsync(Func<Task<bool>> predicate, Func<IError> error)
    {
        return await predicate() ? Success() : Failure(error());
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IResult{T}"/> with the value returned by <paramref name="result"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IResult<T> Bind<T>(Func<IResult<T>> result)
        where T : notnull
    {
        return result();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IResult{T}"/> with the value returned by <paramref name="result"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Task<IResult<T>> Bind<T>(Func<Task<IResult<T>>> result)
        where T : notnull
    {
        return result();
    }

    /// <summary>
    /// Wraps the execution of the given <paramref name="action"/> in a <see cref="Result{T}"/>
    /// catching any thrown exception and returning it as an <see cref="RuntimeError"/> .
    /// </summary>
    public static IResult Execute(Action action)
    {
        try
        {
            action();

            return Success();
        }
        catch (Exception ex)
        {
            RuntimeError error = new(ex);

            return Failure(error);
        }
    }

    /// <summary>
    ///	Wraps the execution of the given <paramref name="action"/> in a <see cref="Result{T}"/>
    /// catching any thrown exception and returning it as an <see cref="RuntimeError"/> .
    /// </summary>
    public static async Task<IResult> Execute(Func<Task> action)
    {
        try
        {
            await action();

            return Success();
        }
        catch (Exception ex)
        {
            RuntimeError error = new(ex);

            return Failure(error);
        }
    }

    /// <summary>
    /// Wraps the execution of the given <paramref name="function"/> in a <see cref="Result{T}"/>
    /// catching any thrown exception and returning it as an <see cref="RuntimeError"/> .
    /// </summary>
    public static IResult<T> Execute<T>(Func<T> function)
        where T : notnull
    {
        try
        {
            var value = function();

            return Success(value);
        }
        catch (Exception ex)
        {
            RuntimeError error = new(ex);

            return Failure<T>(error);
        }
    }

    /// <summary>
    /// Wraps the execution of the given <paramref name="function"/> in a <see cref="Result{T}"/>
    /// catching any thrown exception and returning it as an <see cref="RuntimeError"/> .
    /// </summary>
    public static async Task<IResult<T>> Execute<T>(Func<Task<T>> function)
        where T : notnull
    {
        try
        {
            var value = await function();

            return Success(value);
        }
        catch (Exception ex)
        {
            RuntimeError error = new(ex);

            return Failure<T>(error);
        }
    }

    /// <summary>
    /// Returns a string representation of the <see cref="Result"/>.
    /// </summary>
    /// <returns>A string representing the value.</returns>
    public override string? ToString() => _state.ToString();
}
