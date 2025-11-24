namespace Shared.Application;

/// <summary>
/// Represents the result of some operation, with status information and possibly a value and an error.
/// </summary>
/// <typeparam name="T">The result value type.</typeparam>
public readonly record struct Result<T> : IResult<T>
    where T : notnull
{
    private readonly IResult<T> _state;

    internal Result(IError error)
    {
        _state = new Failure<T>(error);
    }

    internal Result(T value)
    {
        _state = new Success<T>(value);
    }

    public bool IsSuccess => _state.IsSuccess;
    public bool IsFailure => _state.IsFailure;

    /// <summary>
    /// Returns a string representation of the <see cref="Result{T}"/>.
    /// </summary>
    /// <returns>A string representing the value.</returns>
    public override string? ToString() => _state.ToString();
}
