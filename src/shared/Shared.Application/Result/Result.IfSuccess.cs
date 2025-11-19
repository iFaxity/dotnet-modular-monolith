using System.Runtime.CompilerServices;

namespace Shared.Application;

public static partial class ResultExtensions
{
    extension<TResult>(TResult result)
        where TResult : IResult
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult IfSuccess(Action action)
        {
            if (result.IsSuccess)
                action();

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> IfSuccess(Func<Task> action)
        {
            if (result.IsSuccess)
                await action();

            return result;
        }
    }

    extension<T>(IResult<T> result)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IResult<T> IfSuccess(Action<T> action)
        {
            if (Result.TryUnwrap(result, out var value))
                action(value);

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> IfSuccess(Func<T, Task> action)
        {
            if (Result.TryUnwrap(result, out var value))
                await action(value);

            return result;
        }
    }

    extension<T>(Task<IResult<T>> resultTask)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> IfSuccess(Action<T> action)
        {
            var result = await resultTask;

            return result.IfSuccess(action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IResult<T>> IfSuccess(Func<T, Task> action)
        {
            var result = await resultTask;

            return await result.IfSuccess(action);
        }
    }

    extension<TResult>(Task<TResult> resultTask)
        where TResult : IResult
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> IfSuccess(Action action)
        {
            var result = await resultTask;

            return result.IfSuccess(action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> IfSuccess(Func<Task> action)
        {
            var result = await resultTask;

            return await result.IfSuccess(action);
        }
    }

    extension<T>(IEnumerable<IResult<T>> results)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IResult<T>> IfSuccess(Action<T> action)
        {
            foreach (var result in results)
                result.IfSuccess(action);

            return results;
        }
    }

    extension<TResult>(IEnumerable<TResult> results)
        where TResult : IResult
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<TResult> IfSuccess(Action action)
        {
            foreach (var result in results)
                result.IfSuccess(action);

            return results;
        }
    }

    extension<T>(Task<IEnumerable<IResult<T>>> resultsTask)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IEnumerable<IResult<T>>> IfSuccess(Action<T> action)
        {
            var results = await resultsTask;

            return results.IfSuccess(action);
        }
    }

    extension<TResult>(Task<IEnumerable<TResult>> resultsTask)
        where TResult : IResult
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IEnumerable<TResult>> IfSuccess(Action action)
        {
            var results = await resultsTask;

            return results.IfSuccess(action);
        }
    }
}
