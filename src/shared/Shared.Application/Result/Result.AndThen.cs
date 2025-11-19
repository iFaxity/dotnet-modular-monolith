using System.Runtime.CompilerServices;

namespace Shared.Application;

public static partial class ResultExtensions
{
    extension(IResult result)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult AndThen(IResult other)
        {
            if (!Result.TryUnwrapError(result, out var error))
                return other;

            return Result.Failure(error);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult AndThen(Func<IResult> functor)
        {
            if (!Result.TryUnwrapError(result, out var error))
                return functor();

            return Result.Failure(error);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult> AndThen(Func<Task<IResult>> functor)
        {
            if (!Result.TryUnwrapError(result, out var error))
                return await functor();

            return Result.Failure(error);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult<T> AndThen<T>(Func<IResult<T>> functor)
            where T : notnull
        {
            if (!Result.TryUnwrapError(result, out var error))
                return functor();

            return Result.Failure<T>(error);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> AndThen<T>(Func<Task<IResult<T>>> functor)
            where T : notnull
        {
            if (!Result.TryUnwrapError(result, out var error))
                return await functor();

            return Result.Failure<T>(error);
        }
    }

    extension(Task<IResult> resultTask)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult> AndThen(IResult other)
        {
            var result = await resultTask;

            return result.AndThen(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult> AndThen(Func<IResult> functor)
        {
            var result = await resultTask;

            return result.AndThen(functor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult> AndThen(Func<Task<IResult>> functor)
        {
            var result = await resultTask;

            return await result.AndThen(functor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> AndThen<T>(Func<IResult<T>> functor)
            where T : notnull
        {
            var result = await resultTask;

            return result.AndThen(functor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> AndThen<T>(Func<Task<IResult<T>>> functor)
            where T : notnull
        {
            var result = await resultTask;

            return await result.AndThen(functor);
        }
    }

    extension<T>(IResult<T> result)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult<TValue> AndThen<TValue>(Func<T, IResult<TValue>> functor)
            where TValue : notnull
        {
            if (Result.TryUnwrap(result, out var value))
                return functor(value);

            return Result.Failure<TValue>(result.UnwrapError());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<TValue>> AndThen<TValue>(Func<T, Task<IResult<TValue>>> functor)
            where TValue : notnull
        {
            if (Result.TryUnwrap(result, out var value))
                return await functor(value);

            return Result.Failure<TValue>(result.UnwrapError());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult AndThen(Func<T, IResult> functor)
        {
            if (Result.TryUnwrap(result, out var value))
                return functor(value);

            return Result.Failure(result.UnwrapError());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult> AndThen(Func<T, Task<IResult>> functor)
        {
            if (Result.TryUnwrap(result, out var value))
                return await functor(value);

            return Result.Failure(result.UnwrapError());
        }
    }

    extension<T>(Task<IResult<T>> resultTask)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<TValue>> AndThen<TValue>(Func<T, IResult<TValue>> functor)
            where TValue : notnull
        {
            var result = await resultTask;

            return result.AndThen(functor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<TValue>> AndThen<TValue>(Func<T, Task<IResult<TValue>>> functor)
            where TValue : notnull
        {
            var result = await resultTask;

            return await result.AndThen(functor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult> AndThen(Func<T, Task<IResult>> functor)
        {
            var result = await resultTask;

            return await result.AndThen(functor);
        }
    }
}
