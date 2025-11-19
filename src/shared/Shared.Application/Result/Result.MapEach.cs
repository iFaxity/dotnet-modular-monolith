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
        /// Maps values via the functor.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IResult<TValue>> MapEach<TValue>(Func<T, TValue> map)
            where TValue : notnull => results.Select(result => result.Map(map));
    }

    extension<T>(Task<IEnumerable<IResult<T>>> resultsTask)
        where T : notnull
    {
        /// <summary>
        /// Maps values via the functor.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IEnumerable<IResult<TValue>>> MapEach<TValue>(Func<T, TValue> map)
            where TValue : notnull
        {
            var results = await resultsTask;

            return results.MapEach(map);
        }
    }
}
