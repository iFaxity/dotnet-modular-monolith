namespace Shared.Application;

public static partial class ResultExtensions
{
    extension<T>(IResult<T> result)
        where T : notnull
    {
        public IResult<T> Ensure(Func<T, bool> predicate, IError error)
        {
            if (!Result.TryUnwrap(result, out var value))
                return result;

            var match = predicate(value);

            return match ? result : Result.Failure<T>(error);
        }

        public async Task<IResult<T>> Ensure(Func<T, Task<bool>> predicate, IError error)
        {
            if (!Result.TryUnwrap(result, out var value))
                return result;

            var match = await predicate(value);

            return match ? result : Result.Failure<T>(error);
        }

        public IResult<T> Ensure(Func<T, bool> predicate, Func<T, IError> mapError)
        {
            if (!Result.TryUnwrap(result, out var value))
                return result;

            var match = predicate(value);

            if (match)
                return result;

            var error = mapError(value);

            return Result.Failure<T>(error);
        }

        public async Task<IResult<T>> Ensure(
            Func<T, Task<bool>> predicate,
            Func<T, IError> mapError
        )
        {
            if (!Result.TryUnwrap(result, out var value))
                return result;

            var match = await predicate(value);

            if (match)
                return result;

            var error = mapError(value);

            return Result.Failure<T>(error);
        }

        public async Task<IResult<T>> Ensure(
            Func<T, bool> predicate,
            Func<T, Task<IError>> mapError
        )
        {
            if (!Result.TryUnwrap(result, out var value))
                return result;

            var match = predicate(value);

            if (match)
                return result;

            var error = await mapError(value);

            return Result.Failure<T>(error);
        }

        public async Task<IResult<T>> Ensure(
            Func<T, Task<bool>> predicate,
            Func<T, Task<IError>> mapError
        )
        {
            if (!Result.TryUnwrap(result, out var value))
                return result;

            var match = await predicate(value);

            if (match)
                return result;

            var error = await mapError(value);

            return Result.Failure<T>(error);
        }
    }

    extension<T>(Task<IResult<T>> resultTask)
        where T : notnull
    {
        public async Task<IResult<T>> Ensure(Func<T, bool> predicate, IError error)
        {
            var result = await resultTask;

            return result.Ensure(predicate, error);
        }

        public async Task<IResult<T>> Ensure(Func<T, Task<bool>> predicate, IError error)
        {
            var result = await resultTask;

            return await result.Ensure(predicate, error);
        }

        public async Task<IResult<T>> Ensure(Func<T, bool> predicate, Func<T, IError> mapError)
        {
            var result = await resultTask;

            return result.Ensure(predicate, mapError);
        }

        public async Task<IResult<T>> Ensure(
            Func<T, Task<bool>> predicate,
            Func<T, IError> mapError
        )
        {
            var result = await resultTask;

            return await result.Ensure(predicate, mapError);
        }

        public async Task<IResult<T>> Ensure(
            Func<T, bool> predicate,
            Func<T, Task<IError>> mapError
        )
        {
            var result = await resultTask;

            return await result.Ensure(predicate, mapError);
        }

        public async Task<IResult<T>> Ensure(
            Func<T, Task<bool>> predicate,
            Func<T, Task<IError>> mapError
        )
        {
            var result = await resultTask;

            return await result.Ensure(predicate, mapError);
        }
    }
}
