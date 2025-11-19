using System.Runtime.CompilerServices;

namespace Shared.Application;

public static partial class ResultExtensions
{
    extension<T>(IResult<T> result)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Satisfies(Func<T, bool> predicate) => Match(result, predicate, false);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<bool> Satisfies(Func<T, Task<bool>> predicate) =>
            Match(result, predicate, false);
    }

    extension<T>(Task<IResult<T>> resultTask)
        where T : notnull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<bool> Satisfies(Func<T, bool> predicate)
        {
            var result = await resultTask;

            return result.Match(predicate, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<bool> Satisfies(Func<T, Task<bool>> predicate) =>
            resultTask.Match(predicate, false);
    }
}
