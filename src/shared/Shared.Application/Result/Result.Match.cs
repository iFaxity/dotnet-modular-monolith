using System.Runtime.CompilerServices;

namespace Shared.Application;

public static partial class ResultExtensions
{
    extension(IResult result)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Match<TResult>(TResult onSuccess, TResult onFailure) =>
            result.IsSuccess ? onSuccess : onFailure;

        public TResult Match<TResult>(Func<TResult> onSuccess, TResult onFailure) =>
            result.IsSuccess ? onSuccess() : onFailure;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Match<TResult>(TResult onSuccess, Func<TResult> onFailure) =>
            result.IsSuccess ? onSuccess : onFailure();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Match<TResult>(Func<TResult> onSuccess, Func<TResult> onFailure) =>
            result.IsSuccess ? onSuccess() : onFailure();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Match<TResult>(TResult onSuccess, Func<IError, TResult> onFailure) =>
            result.Match(() => onSuccess, onFailure);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Match<TResult>(Func<TResult> onSuccess, Func<IError, TResult> onFailure)
        {
            if (!Result.TryUnwrapError(result, out var error))
                return onSuccess();

            return onFailure(error);
        }
    }

    extension<T>(IResult<T> result)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Match<TResult>(Func<T, TResult> onSuccess, TResult onFailure) =>
            result.Match(onSuccess, _ => onFailure);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<IError, TResult> onFailure)
        {
            if (Result.TryUnwrap(result, out var value))
                return onSuccess(value);

            return onFailure(result.UnwrapError());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TResult> Match<TResult>(Func<T, Task<TResult>> onSuccess, TResult onFailure) =>
            result.Match(onSuccess, _ => Task.FromResult(onFailure));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TResult> Match<TResult>(
            Func<T, Task<TResult>> onSuccess,
            Func<IError, TResult> onFailure
        ) => result.Match(onSuccess, (error) => Task.FromResult(onFailure(error)));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TResult> Match<TResult>(
            Func<T, TResult> onSuccess,
            Func<IError, Task<TResult>> onFailure
        ) => result.Match((value) => Task.FromResult(onSuccess(value)), onFailure);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TResult> Match<TResult>(
            Func<T, Task<TResult>> onSuccess,
            Func<IError, Task<TResult>> onFailure
        )
        {
            if (Result.TryUnwrap(result, out var value))
                return onSuccess(value);

            return onFailure(result.UnwrapError());
        }
    }

    extension(Task<IResult> resultTask)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> Match<TResult>(TResult onSuccess, TResult onFailure)
        {
            var result = await resultTask;

            return result.Match(onSuccess, onFailure);
        }

        public async Task<TResult> Match<TResult>(Func<TResult> onSuccess, TResult onFailure)
        {
            var result = await resultTask;

            return result.Match(onSuccess, onFailure);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> Match<TResult>(TResult onSuccess, Func<TResult> onFailure)
        {
            var result = await resultTask;

            return result.Match(onSuccess, onFailure);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> Match<TResult>(Func<TResult> onSuccess, Func<TResult> onFailure)
        {
            var result = await resultTask;

            return result.Match(onSuccess, onFailure);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> Match<TResult>(
            TResult onSuccess,
            Func<IError, TResult> onFailure
        )
        {
            var result = await resultTask;

            return result.Match(onSuccess, onFailure);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> Match<TResult>(
            Func<TResult> onSuccess,
            Func<IError, TResult> onFailure
        )
        {
            var result = await resultTask;

            return result.Match(onSuccess, onFailure);
        }
    }

    extension<T>(Task<IResult<T>> resultTask)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> Match<TResult>(Func<TResult> onSuccess, TResult onFailure)
        {
            var result = await resultTask;

            return result.Match(onSuccess, onFailure);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> Match<TResult>(
            Func<TResult> onSuccess,
            Func<IError, TResult> onFailure
        )
        {
            var result = await resultTask;

            return result.Match(onSuccess, onFailure);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> Match<TResult>(
            Func<T, Task<TResult>> onSuccess,
            TResult onFailure
        )
        {
            var result = await resultTask;

            return await result.Match(onSuccess, onFailure);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> Match<TResult>(
            Func<T, Task<TResult>> onSuccess,
            Func<IError, TResult> onFailure
        )
        {
            var result = await resultTask;

            return await result.Match(onSuccess, onFailure);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> Match<TResult>(
            Func<TResult> onSuccess,
            Func<IError, Task<TResult>> onFailure
        )
        {
            var result = await resultTask;

            return await result.Match(onSuccess, onFailure);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> Match<TResult>(
            Func<T, Task<TResult>> onSuccess,
            Func<IError, Task<TResult>> onFailure
        )
        {
            var result = await resultTask;

            return await result.Match(onSuccess, onFailure);
        }
    }
}
