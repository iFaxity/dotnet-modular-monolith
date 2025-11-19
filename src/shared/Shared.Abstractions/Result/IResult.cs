namespace Shared.Abstractions;

public interface IResult
{
    bool IsSuccess { get; }
    bool IsFailure { get; }
    IError UnwrapError();
}

public interface IResult<out T> : IResult, IEnumerable<T>
    where T : notnull, allows ref struct
{
    T Unwrap();
}
