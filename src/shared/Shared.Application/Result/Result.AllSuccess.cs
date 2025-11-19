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
        /// Determines if all <see cref="IResult"/>s of a sequence has succeeded.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AllSuccess() => results.All(result => result.IsSuccess);
    }

    extension(Task<IEnumerable<IResult>> resultsTask)
    {
        /// <summary>
        /// Determines if all <see cref="IResult"/>s of a sequence has succeeded.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<bool> AllSuccess()
        {
            var results = await resultsTask;

            return results.AllSuccess();
        }
    }
}
