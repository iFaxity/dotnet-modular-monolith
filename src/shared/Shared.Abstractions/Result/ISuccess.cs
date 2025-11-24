namespace Shared.Abstractions;

public interface ISuccess : IResult { }

public interface ISuccess<out T> : IResult<T>, ISuccess
    where T : notnull
{
    T Value { get; }
}
