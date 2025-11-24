using System.Runtime.CompilerServices;

namespace Shared.Application;

public static partial class ResultExtensions
{
    extension<TResult>(TResult result)
        where TResult : IResult
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult IfFailure(Action action)
        {
            if (result.IsFailure)
                action();

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult IfFailure(Action<IError> action)
        {
            if (result is IFailure failure)
                action(failure.Error);

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> IfFailure(Func<Task> action)
        {
            if (result.IsFailure)
                await action();

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> IfFailure(Func<IError, Task> action)
        {
            if (result is IFailure failure)
                await action(failure.Error);

            return result;
        }
    }

    extension<TResult>(Task<TResult> resultTask)
        where TResult : IResult
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> IfFailure(Action action)
        {
            var result = await resultTask;

            return result.IfFailure(action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> IfFailure(Action<IError> action)
        {
            var result = await resultTask;

            return result.IfFailure(action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> IfFailure(Func<Task> action)
        {
            var result = await resultTask;

            return await result.IfFailure(action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> IfFailure(Func<IError, Task> action)
        {
            var result = await resultTask;

            return await result.IfFailure(action);
        }
    }

    extension<TResult>(IEnumerable<TResult> results)
        where TResult : IResult
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<TResult> IfFailure(Action action)
        {
            foreach (var result in results)
                result.IfFailure(action);

            return results;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<TResult> IfFailure(Action<IError> action)
        {
            foreach (var result in results)
                result.IfFailure(action);

            return results;
        }
    }

    extension<TResult>(Task<IEnumerable<TResult>> resultsTask)
        where TResult : IResult
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IEnumerable<TResult>> IfFailure(Action action)
        {
            var results = await resultsTask;

            return results.IfFailure(action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IEnumerable<TResult>> IfFailure(Action<IError> action)
        {
            var results = await resultsTask;

            return results.IfFailure(action);
        }
    }
}
