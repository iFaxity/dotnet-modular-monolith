using System.Runtime.CompilerServices;

namespace Shared.Application;

public static partial class ResultExtensions
{
    extension(IResult result)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult MapError(IError error)
        {
            if (result.IsSuccess)
                return result;

            return Result.Failure(error);
        }
    }

    extension<T>(IResult<T> result)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult<T> MapError(IError error)
        {
            if (result.IsSuccess)
                return result;

            return Result.Failure<T>(error);
        }

        /// <summary>
        /// In case of failure return the <paramref name="map"/> result.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult<T> MapError(Func<IError> map)
        {
            if (result.IsSuccess)
                return result;

            var mappedError = map();

            return Result.Failure<T>(mappedError);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult<T> MapError(Func<IError, IError> map)
        {
            if (result is not IFailure<T> failure)
                return result;

            var mappedError = map(failure.Error);

            return Result.Failure<T>(mappedError);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> MapError(Func<Task<IError>> map)
        {
            if (result.IsSuccess)
                return result;

            var mappedError = await map();

            return Result.Failure<T>(mappedError);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> MapError(Func<IError, Task<IError>> map)
        {
            if (result is not IFailure<T> failure)
                return result;

            var mappedError = await map(failure.Error);

            return Result.Failure<T>(mappedError);
        }
    }

    extension(Task<IResult> resultTask)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult> MapError(IError error)
        {
            var result = await resultTask;

            return result.MapError(error);
        }
    }

    extension<T>(Task<IResult<T>> resultTask)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> MapError(IError error)
        {
            var result = await resultTask;

            return result.MapError(error);
        }

        /// <summary>
        /// In case of failure return the <paramref name="map"/> result.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> MapError(Func<IError> map)
        {
            var result = await resultTask;

            return result.MapError(map);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> MapError(Func<IError, IError> map)
        {
            var result = await resultTask;

            return result.MapError(map);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> MapError(Func<Task<IError>> map)
        {
            var result = await resultTask;

            return await result.MapError(map);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> MapError(Func<IError, Task<IError>> map)
        {
            var result = await resultTask;

            return await result.MapError(map);
        }
    }
}
