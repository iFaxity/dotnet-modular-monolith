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
        public TResult Match<TResult>(TResult onSuccess, Func<IError, TResult> onFailure)
        {
            return result switch
            {
                ISuccess => onSuccess,
                IFailure failure => onFailure(failure.Error),
                _ => throw new InvalidOperationException(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Match<TResult>(Func<TResult> onSuccess, Func<IError, TResult> onFailure)
        {
            return result switch
            {
                ISuccess => onSuccess(),
                IFailure failure => onFailure(failure.Error),
                _ => throw new InvalidOperationException(),
            };
        }
    }

    extension<T>(IResult<T> result)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Match<TResult>(Func<T, TResult> onSuccess, TResult onFailure)
        {
            return result switch
            {
                ISuccess<T> success => onSuccess(success.Value),
                IFailure => onFailure,
                _ => throw new InvalidOperationException(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<IError, TResult> onFailure)
        {
            return result switch
            {
                ISuccess<T> success => onSuccess(success.Value),
                IFailure failure => onFailure(failure.Error),
                _ => throw new InvalidOperationException(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TResult> Match<TResult>(Func<T, Task<TResult>> onSuccess, TResult onFailure)
        {
            return result switch
            {
                ISuccess<T> success => onSuccess(success.Value),
                IFailure => Task.FromResult(onFailure),
                _ => throw new InvalidOperationException(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TResult> Match<TResult>(
            Func<T, Task<TResult>> onSuccess,
            Func<IError, TResult> onFailure
        )
        {
            return result switch
            {
                ISuccess<T> success => onSuccess(success.Value),
                IFailure failure => Task.FromResult(onFailure(failure.Error)),
                _ => throw new InvalidOperationException(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TResult> Match<TResult>(
            Func<T, TResult> onSuccess,
            Func<IError, Task<TResult>> onFailure
        )
        {
            return result switch
            {
                ISuccess<T> success => Task.FromResult(onSuccess(success.Value)),
                IFailure failure => onFailure(failure.Error),
                _ => throw new InvalidOperationException(),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TResult> Match<TResult>(
            Func<T, Task<TResult>> onSuccess,
            Func<IError, Task<TResult>> onFailure
        )
        {
            return result switch
            {
                ISuccess<T> success => onSuccess(success.Value),
                IFailure failure => onFailure(failure.Error),
                _ => throw new InvalidOperationException(),
            };
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
