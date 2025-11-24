namespace Shared.Application;

public static partial class ResultExtensions
{
    extension<T>(IResult<T> result)
        where T : notnull
    {
        public IResult<T> Ensure(Func<T, bool> predicate, IError error)
        {
            if (result is not ISuccess<T> success)
                return result;

            var match = predicate(success.Value);

            return match ? result : Result.Failure<T>(error);
        }

        public async Task<IResult<T>> Ensure(Func<T, Task<bool>> predicate, IError error)
        {
            if (result is not ISuccess<T> success)
                return result;

            var match = await predicate(success.Value);

            return match ? result : Result.Failure<T>(error);
        }

        public IResult<T> Ensure(Func<T, bool> predicate, Func<T, IError> mapError)
        {
            if (result is not ISuccess<T> success)
                return result;

            var match = predicate(success.Value);

            if (match)
                return result;

            var error = mapError(success.Value);

            return Result.Failure<T>(error);
        }

        public async Task<IResult<T>> Ensure(
            Func<T, Task<bool>> predicate,
            Func<T, IError> mapError
        )
        {
            if (result is not ISuccess<T> success)
                return result;

            var match = await predicate(success.Value);

            if (match)
                return result;

            var error = mapError(success.Value);

            return Result.Failure<T>(error);
        }

        public async Task<IResult<T>> Ensure(
            Func<T, bool> predicate,
            Func<T, Task<IError>> mapError
        )
        {
            if (result is not ISuccess<T> success)
                return result;

            var match = predicate(success.Value);

            if (match)
                return result;

            var error = await mapError(success.Value);

            return Result.Failure<T>(error);
        }

        public async Task<IResult<T>> Ensure(
            Func<T, Task<bool>> predicate,
            Func<T, Task<IError>> mapError
        )
        {
            if (result is not ISuccess<T> success)
                return result;

            var match = await predicate(success.Value);

            if (match)
                return result;

            var error = await mapError(success.Value);

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
