namespace Shared.Abstractions;

public interface IFailure : IResult
{
    IError Error { get; }
}

public interface IFailure<out T> : IResult<T>, IFailure
    where T : notnull { }
