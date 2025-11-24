namespace Shared.Abstractions;

public interface IResult
{
    /// <summary>
    /// Gets a value indicating whether the result is a success result.
    /// </summary>
    bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the result is a failure result.
    /// </summary>
    bool IsFailure { get; }
}

public interface IResult<out T> : IResult
    where T : notnull { }
