using System.Runtime.CompilerServices;

namespace Shared.Application;

public static partial class ResultExtensions
{
    extension<TResult>(TResult result)
        where TResult : IResult
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Execute(Action action)
        {
            action();

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Execute(Action<TResult> action)
        {
            action(result);

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> Execute(Func<Task> action)
        {
            await action();

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> Execute(Func<TResult, Task> action)
        {
            await action(result);

            return result;
        }
    }

    extension<TResult>(Task<TResult> resultTask)
        where TResult : IResult
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> Execute(Action action)
        {
            var result = await resultTask;

            return result.Execute(action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> Execute(Action<TResult> action)
        {
            var result = await resultTask;

            return result.Execute(action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> Execute(Func<Task> action)
        {
            var result = await resultTask;

            return await result.Execute(action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> Execute(Func<TResult, Task> action)
        {
            var result = await resultTask;

            return await result.Execute(action);
        }
    }
}
