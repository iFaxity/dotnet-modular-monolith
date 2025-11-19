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
        public IEnumerable<IResult<T>> OrElseEach(Func<IError, IResult<T>> functor) =>
            results.Select(result => result.OrElse(functor));
    }

    extension<T>(Task<IEnumerable<IResult<T>>> resultsTask)
        where T : notnull
    {
        /// <summary>
        /// Bind values via the functor.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IEnumerable<IResult<T>>> OrElseEach(Func<IError, IResult<T>> functor)
        {
            var results = await resultsTask;

            return results.OrElseEach(functor);
        }
    }
}
