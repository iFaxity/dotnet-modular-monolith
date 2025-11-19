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
        /// Matches results via the two functors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<TResult> MatchEach<TResult>(Func<T, TResult> mapSome, TResult none)
            where TResult : notnull => results.Select(result => result.Match(mapSome, none));
    }

    extension<T>(Task<IEnumerable<IResult<T>>> resultsTask)
        where T : notnull
    {
        /// <summary>
        /// Matches results via the two functors.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IEnumerable<TResult>> MatchEach<TResult>(
            Func<T, TResult> mapSome,
            TResult none
        )
            where TResult : notnull
        {
            var results = await resultsTask;

            return results.MatchEach(mapSome, none);
        }
    }
}
