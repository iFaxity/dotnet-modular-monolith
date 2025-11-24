using System.Runtime.CompilerServices;

namespace Shared.Application;

public static partial class ResultExtensions
{
    extension(IResult result)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult AndThen(IResult other)
        {
            return result switch
            {
                ISuccess => other,
                IFailure failure => Result.Failure(failure.Error),
                _ => throw new InvalidOperationException(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult AndThen(Func<IResult> functor)
        {
            return result switch
            {
                ISuccess => functor(),
                IFailure failure => Result.Failure(failure.Error),
                _ => throw new InvalidOperationException(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult> AndThen(Func<Task<IResult>> functor)
        {
            return result switch
            {
                ISuccess => await functor(),
                IFailure failure => Result.Failure(failure.Error),
                _ => throw new InvalidOperationException(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult<T> AndThen<T>(Func<IResult<T>> functor)
            where T : notnull
        {
            return result switch
            {
                ISuccess => functor(),
                IFailure failure => Result.Failure<T>(failure.Error),
                _ => throw new InvalidOperationException(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> AndThen<T>(Func<Task<IResult<T>>> functor)
            where T : notnull
        {
            return result switch
            {
                ISuccess => await functor(),
                IFailure failure => Result.Failure<T>(failure.Error),
                _ => throw new InvalidOperationException(),
            };
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
            return result switch
            {
                ISuccess<T> success => functor(success.Value),
                IFailure failure => Result.Failure<TValue>(failure.Error),
                _ => throw new InvalidOperationException(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<TValue>> AndThen<TValue>(Func<T, Task<IResult<TValue>>> functor)
            where TValue : notnull
        {
            return result switch
            {
                ISuccess<T> success => await functor(success.Value),
                IFailure failure => Result.Failure<TValue>(failure.Error),
                _ => throw new InvalidOperationException(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult AndThen(Func<T, IResult> functor)
        {
            return result switch
            {
                ISuccess<T> success => functor(success.Value),
                IFailure failure => Result.Failure(failure.Error),
                _ => throw new InvalidOperationException(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult> AndThen(Func<T, Task<IResult>> functor)
        {
            return result switch
            {
                ISuccess<T> success => await functor(success.Value),
                IFailure failure => Result.Failure(failure.Error),
                _ => throw new InvalidOperationException(),
            };
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
