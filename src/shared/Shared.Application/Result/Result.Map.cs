using System.Runtime.CompilerServices;

namespace Shared.Application;

public static partial class ResultExtensions
{
    extension(IResult result)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult<TValue> Map<TValue>(TValue value)
            where TValue : notnull
        {
            return Match(result, () => Result.Success(value), Result.Failure<TValue>);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult<TValue> Map<TValue>(Func<TValue> mapValue)
            where TValue : notnull
        {
            IResult<TValue> mapSuccess()
            {
                var mappedValue = mapValue();

                return Result.Success(mappedValue);
            }

            return Match(result, mapSuccess, Result.Failure<TValue>);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<IResult<TValue>> Map<TValue>(Func<Task<TValue>> mapValue)
            where TValue : notnull
        {
            async Task<IResult<TValue>> mapSuccess()
            {
                var mappedValue = await mapValue();

                return Result.Success(mappedValue);
            }

            return Match(
                result,
                mapSuccess,
                (error) => Task.FromResult(Result.Failure<TValue>(error))
            );
        }
    }

    extension(Task<IResult> resultTask)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<TValue>> Map<TValue>(TValue value)
            where TValue : notnull
        {
            var result = await resultTask;

            return result.Map(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<TValue>> Map<TValue>(Func<TValue> mapValue)
            where TValue : notnull
        {
            var result = await resultTask;

            return result.Map(mapValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<TValue>> Map<TValue>(Func<Task<TValue>> mapValue)
            where TValue : notnull
        {
            var result = await resultTask;

            return await result.Map(mapValue);
        }
    }

    extension<T>(IResult<T> result)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult<TValue> Map<TValue>(TValue value)
            where TValue : notnull
        {
            return Match(result, () => Result.Success(value), Result.Failure<TValue>);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult<TValue> Map<TValue>(Func<TValue> mapValue)
            where TValue : notnull
        {
            IResult<TValue> mapSuccess()
            {
                var mappedValue = mapValue();

                return Result.Success(mappedValue);
            }

            return Match(result, mapSuccess, Result.Failure<TValue>);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<IResult<TValue>> Map<TValue>(Func<Task<TValue>> mapValue)
            where TValue : notnull
        {
            async Task<IResult<TValue>> mapSuccess()
            {
                var mappedValue = await mapValue();

                return Result.Success(mappedValue);
            }

            return Match(
                result,
                mapSuccess,
                (error) => Task.FromResult(Result.Failure<TValue>(error))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult<TValue> Map<TValue>(Func<T, TValue> mapValue)
            where TValue : notnull
        {
            IResult<TValue> mapSuccess(T value)
            {
                var mappedValue = mapValue(value);

                return Result.Success(mappedValue);
            }

            return Match(result, mapSuccess, Result.Failure<TValue>);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<IResult<TValue>> Map<TValue>(Func<T, Task<TValue>> mapValue)
            where TValue : notnull
        {
            async Task<IResult<TValue>> mapSuccess(T value)
            {
                var mappedValue = await mapValue(value);

                return Result.Success(mappedValue);
            }

            return Match(
                result,
                mapSuccess,
                (error) => Task.FromResult(Result.Failure<TValue>(error))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult Map(Action<T> mapValue)
        {
            IResult mapSuccess(T value)
            {
                mapValue(value);

                return Result.Success();
            }

            return Match(result, mapSuccess, Result.Failure);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<IResult> Map(Func<T, Task> mapValue)
        {
            async Task<IResult> mapSuccess(T value)
            {
                await mapValue(value);

                return Result.Success();
            }

            return Match(result, mapSuccess, Result.Failure);
        }
    }

    extension<T>(Task<IResult<T>> resultTask)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<TValue>> Map<TValue>(TValue value)
            where TValue : notnull
        {
            var result = await resultTask;

            return result.Map(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<TValue>> Map<TValue>(Func<TValue> mapValue)
            where TValue : notnull
        {
            var result = await resultTask;

            return result.Map(mapValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<TValue>> Map<TValue>(Func<Task<TValue>> mapValue)
            where TValue : notnull
        {
            var result = await resultTask;

            return await result.Map(mapValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<TValue>> Map<TValue>(Func<T, Task<TValue>> mapValue)
            where TValue : notnull
        {
            var result = await resultTask;

            return await result.Map(mapValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult> Map(Action<T> mapValue)
        {
            var result = await resultTask;

            return result.Map(mapValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult> Map(Func<T, Task> mapValue)
        {
            var result = await resultTask;

            return await result.Map(mapValue);
        }
    }
}
