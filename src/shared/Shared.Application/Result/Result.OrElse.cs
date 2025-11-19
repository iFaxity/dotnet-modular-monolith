using System.Runtime.CompilerServices;

namespace Shared.Application;

public static partial class ResultExtensions
{
    extension<TResult>(TResult result)
        where TResult : IResult
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult OrElse(Func<TResult> functor) => Match(result, () => result, functor);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult OrElse(Func<IError, TResult> functor) =>
            Match(result, () => result, functor);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TResult> OrElse(Func<Task<TResult>> functor) =>
            Match(result, Task.FromResult(result), functor);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TResult> OrElse(Func<IError, Task<TResult>> functor) =>
            Match(result, Task.FromResult(result), functor);
    }

    extension<TResult>(Task<TResult> resultTask)
        where TResult : IResult
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> OrElse(Func<TResult> functor)
        {
            var result = await resultTask;

            return result.OrElse(functor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> OrElse(Func<IError, TResult> functor)
        {
            var result = await resultTask;

            return result.OrElse(functor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> OrElse(Func<Task<TResult>> functor)
        {
            var result = await resultTask;

            return await result.OrElse(functor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<TResult> OrElse(Func<IError, Task<TResult>> functor)
        {
            var result = await resultTask;

            return await result.OrElse(functor);
        }
    }
}
