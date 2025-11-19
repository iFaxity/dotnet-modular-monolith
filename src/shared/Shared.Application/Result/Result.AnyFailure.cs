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
        /// Determines if any <see cref="IResult"/>s of a sequence has failed.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AnyFailure() => results.Any(result => result.IsFailure);
    }

    extension(Task<IEnumerable<IResult>> resultsTask)
    {
        /// <summary>
        /// Determines if any <see cref="IResult"/>s of a sequence has failed.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<bool> AnyFailure()
        {
            var results = await resultsTask;

            return results.AnyFailure();
        }
    }
}
