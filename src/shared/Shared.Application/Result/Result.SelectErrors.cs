using System.Runtime.CompilerServices;

namespace Shared.Application;

/// <summary>
/// Extension methods for the <see cref="IResult{T}"/> type.
/// </summary>
public static partial class ResultExtensions
{
    extension(IEnumerable<IResult> results)
    {
        /// <summary>
        /// Extract errors from <see cref="IResult"/>s.
        /// Successful results are discarded.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IError> SelectErrors() =>
            results.OfType<IFailure>().Select(failure => failure.Error);
    }

    extension(Task<IEnumerable<IResult>> resultsTask)
    {
        /// <summary>
        /// Extract errors from <see cref="IResult"/>s.
        /// Successful results are discarded.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<IEnumerable<IError>> SelectErrors()
        {
            var results = await resultsTask;

            return results.SelectErrors();
        }
    }
}
