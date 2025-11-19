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
        /// Extract values from <see cref="IResult{T}"/>s.
        /// Failed results are discarded.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> SelectValues() => results.SelectMany(result => result);
    }

    extension<T>(Task<IEnumerable<IResult<T>>> resultsTask)
        where T : notnull
    {
        /// <summary>
        /// Extract values from <see cref="IResult{T}"/>s.
        /// Failed results are discarded.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IEnumerable<T>> SelectValues()
        {
            var results = await resultsTask;

            return results.SelectValues();
        }
    }
}
