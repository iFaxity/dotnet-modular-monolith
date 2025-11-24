using System.Runtime.CompilerServices;

namespace Shared.Application;

public static partial class ResultExtensions
{
    extension<T>(IResult<T> result)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult<T> RecoverWith(T newValue)
        {
            if (result.IsSuccess)
                return result;

            return Result.Success(newValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult<T> RecoverWith(Func<T> mapValue)
        {
            if (result.IsSuccess)
                return result;

            var mappedValue = mapValue();

            return Result.Success(mappedValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult<T> RecoverWith(Func<IError, T> mapValue)
        {
            if (result is not IFailure failure)
                return result;

            var mappedValue = mapValue(failure.Error);

            return Result.Success(mappedValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> RecoverWith(Func<Task<T>> mapValue)
        {
            if (result.IsSuccess)
                return result;

            var mappedValue = await mapValue();

            return Result.Success(mappedValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> RecoverWith(Func<IError, Task<T>> mapValue)
        {
            if (result is not IFailure failure)
                return result;

            var mappedValue = await mapValue(failure.Error);

            return Result.Success(mappedValue);
        }
    }

    extension<T>(Task<IResult<T>> resultTask)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> RecoverWith(T newValue)
        {
            var result = await resultTask;

            return result.RecoverWith(newValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> RecoverWith(Func<T> mapValue)
        {
            var result = await resultTask;

            return result.RecoverWith(mapValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> RecoverWith(Func<IError, T> mapValue)
        {
            var result = await resultTask;

            return result.RecoverWith(mapValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> RecoverWith(Func<Task<T>> mapValue)
        {
            var result = await resultTask;

            return await result.RecoverWith(mapValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> RecoverWith(Func<IError, Task<T>> mapValue)
        {
            var result = await resultTask;

            return await result.RecoverWith(mapValue);
        }
    }
}
