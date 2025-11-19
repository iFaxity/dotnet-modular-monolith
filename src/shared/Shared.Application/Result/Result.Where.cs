using System.Runtime.CompilerServices;

namespace Shared.Application;

/// <summary>
/// Extension methods for the <see cref="IResult{T}"/> type.
/// </summary>
public static partial class ResultExtensions
{
    extension<T>(IEnumerable<IResult<T>> results)
        where T : notnull
    {
        /// <summary>
        /// Filters successful results via the predicate.
        /// Failing results are discarded.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IResult<T>> Where(Func<T, bool> predicate) =>
            SelectValues(results).Where(predicate).Select(Result.Success);
    }

    extension<T>(Task<IEnumerable<IResult<T>>> resultsTask)
        where T : notnull
    {
        /// <summary>
        /// Filters successful results via the predicate.
        /// Failing results are discarded.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IEnumerable<IResult<T>>> Where(Func<T, bool> predicate)
        {
            var results = await resultsTask;

            return results.Where(predicate);
        }
    }
}
