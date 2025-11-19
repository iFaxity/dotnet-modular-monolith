using System.Collections;

namespace Shared.Application;

/// <summary>
/// Represents the result of some operation, with status information and possibly a value and an error.
/// </summary>
/// <typeparam name="T">The result value type.</typeparam>
public readonly record struct Result<T> : IResult<T>
    where T : notnull
{
    private readonly IError _error;
    private readonly T? _value;

    internal Result(IError error)
    {
        if (DomainError.IsNullOrNone(error))
            throw new ArgumentException("Error must be set when value is null", nameof(error));

        _error = error;
    }

    internal Result(T? value)
    {
        _value = value;
        _error = DomainError.None;
    }

    /// <summary>
    /// Gets a value indicating whether the result is a success result.
    /// </summary>
    public bool IsSuccess => DomainError.IsNullOrNone(_error);

    /// <summary>
    /// Gets a value indicating whether the result is a failure result.
    /// </summary>
    public bool IsFailure => !DomainError.IsNullOrNone(_error);

    public static implicit operator Result<T>(T value)
    {
        return new(value);
    }

    public static implicit operator Result<T>(DomainError error)
    {
        return new(error);
    }

    /// <summary>
    /// Gets the result value if the result is successful, otherwise throws an exception.
    /// </summary>
    /// <returns>The result value if the result is successful.</returns>
    /// <exception cref="InvalidOperationException"> when <see cref="Result.IsFailure"/> is <c>true</c>.</exception>
    public T Unwrap()
    {
        if (IsFailure)
            throw new InvalidOperationException(
                "The value of a failure result can not be accessed."
            );

        return _value!;
    }

    /// <summary>
    /// Gets the result error if the result is a failure, otherwise throws an exception.
    /// </summary>
    /// <returns>The result error if the result is a failure.</returns>
    /// <exception cref="InvalidOperationException"> when <see cref="IsSuccess"/> is <c>true</c>.</exception>
    public IError UnwrapError()
    {
        if (DomainError.IsNullOrNone(_error))
            throw new InvalidOperationException(
                "The error of a successful result can not be accessed."
            );

        return _error;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the value if it is present.
    /// </summary>
    /// <returns>An enumerator for the value or an empty enumerator if <see cref="Result.IsFailure"/> is <c>true</c>.</returns>
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        if (_value is null)
            yield break;

        yield return _value;
    }

    /// <summary>
    /// Returns a non-generic enumerator that iterates through the value if it is present.
    /// </summary>
    /// <returns>An enumerator for the value or an empty enumerator if <see cref="Result.IsFailure"/> is <c>true</c>.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<T>)this).GetEnumerator();
    }

    /// <summary>
    /// Returns a string representation of the <see cref="Result{T}"/>.
    /// </summary>
    /// <returns>A string representing the value.</returns>
    public override string ToString()
    {
        return IsSuccess ? $"Success({_value})" : $"Failure({_error.Code}): {_error.Message}";
    }
}
