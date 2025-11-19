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
        /// Bind values via the functor.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IResult<TValue>> AndThenEach<TValue>(Func<T, IResult<TValue>> functor)
            where TValue : notnull => results.Select(result => result.AndThen(functor));

        /// <summary>
        /// Bind values via the functor.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IResult> AndThenEach(Func<T, IResult> functor) =>
            results.Select(result => result.AndThen(functor));
    }

    extension<T>(Task<IEnumerable<IResult<T>>> resultsTask)
        where T : notnull
    {
        /// <summary>
        /// Bind values via the functor.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IEnumerable<IResult<TValue>>> AndThenEach<TValue>(
            Func<T, IResult<TValue>> functor
        )
            where TValue : notnull
        {
            var results = await resultsTask;

            return results.AndThenEach(functor);
        }
    }
}
